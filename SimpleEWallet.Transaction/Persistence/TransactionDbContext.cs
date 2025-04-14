using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Transaction.Domain;

namespace SimpleEWallet.Transaction.Persistence;

public partial class TransactionDbContext : DbContext
{
    public TransactionDbContext()
    {
    }

    public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstConfig> MstConfigs { get; set; }

    public virtual DbSet<MstTransactionType> MstTransactionTypes { get; set; }

    public virtual DbSet<TrnTransaction> TrnTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=admin_db;Password=admin;Server=localhost;Port=5432;Database=ewallet_db_transaction;Pooling=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_config_pkey");

            entity.ToTable("mst_config");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, 9999999L, true, null)
                .HasColumnName("id");
            entity.Property(e => e.ConfigKey)
                .HasMaxLength(500)
                .HasColumnName("config_key");
            entity.Property(e => e.ConfigValue).HasColumnName("config_value");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_modified_at");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
        });

        modelBuilder.Entity<MstTransactionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_transaction_type_pkey");

            entity.ToTable("mst_transaction_type");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, 9L, true, null)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_modified_at");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TrnTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trn_transaction_pkey");

            entity.ToTable("trn_transaction");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_modified_at");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.Reference).HasColumnName("reference");
            entity.Property(e => e.TransactionTypeId)
                .HasDefaultValue(1)
                .HasColumnName("transaction_type_id");
            entity.Property(e => e.WalletId).HasColumnName("wallet_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
