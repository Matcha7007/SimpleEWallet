using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Wallet.Domain;

namespace SimpleEWallet.Wallet.Persistence;

public partial class WalletDbContext : DbContext
{
    public WalletDbContext()
    {
    }

    public WalletDbContext(DbContextOptions<WalletDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstConfig> MstConfigs { get; set; }

    public virtual DbSet<MstStatus> MstStatuses { get; set; }

    public virtual DbSet<MstWallet> MstWallets { get; set; }

    public virtual DbSet<TrnTopupRequest> TrnTopupRequests { get; set; }

    public virtual DbSet<TrnTransfer> TrnTransfers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=admin_db;Password=admin;Server=localhost;Port=5432;Database=ewallet_db_wallet;Pooling=true;");

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

        modelBuilder.Entity<MstStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_status_pkey");

            entity.ToTable("mst_status");

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
                .HasMaxLength(25)
                .HasColumnName("name");
        });

        modelBuilder.Entity<MstWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_wallet_pkey");

            entity.ToTable("mst_wallet");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .HasColumnName("balance");
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
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WalletName)
                .HasMaxLength(100)
                .HasColumnName("wallet_name");
            entity.Property(e => e.WalletNumber)
                .HasMaxLength(14)
                .HasColumnName("wallet_number");
        });

        modelBuilder.Entity<TrnTopupRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trn_topup_request_pkey");

            entity.ToTable("trn_topup_request");

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
            entity.Property(e => e.StatusId)
                .HasDefaultValue(1)
                .HasColumnName("status_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.WalletId).HasColumnName("wallet_id");

            entity.HasOne(d => d.Status).WithMany(p => p.TrnTopupRequests)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_status_id");

            entity.HasOne(d => d.Wallet).WithMany(p => p.TrnTopupRequests)
                .HasForeignKey(d => d.WalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wallet_id");
        });

        modelBuilder.Entity<TrnTransfer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trn_transfer_pkey");

            entity.ToTable("trn_transfer");

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
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_modified_at");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.ReceiverWalletId).HasColumnName("receiver_wallet_id");
            entity.Property(e => e.SenderWalletId).HasColumnName("sender_wallet_id");
            entity.Property(e => e.StatusId)
                .HasDefaultValue(1)
                .HasColumnName("status_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

            entity.HasOne(d => d.ReceiverWallet).WithMany(p => p.TrnTransferReceiverWallets)
                .HasForeignKey(d => d.ReceiverWalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_receiver_wallet_id");

            entity.HasOne(d => d.SenderWallet).WithMany(p => p.TrnTransferSenderWallets)
                .HasForeignKey(d => d.SenderWalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sender_wallet_id");

            entity.HasOne(d => d.Status).WithMany(p => p.TrnTransfers)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_status_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
