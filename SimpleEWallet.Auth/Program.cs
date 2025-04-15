using MassTransit;

using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Auth.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region MassTransit
builder.Services.AddMassTransit(regisConfig =>
{
	regisConfig.UsingRabbitMq((context, cfg) =>
	{
		// default ke host localhost
		cfg.Host("rabbitmq_wallet", "/", hostConfig =>
		{
			hostConfig.Username("guest");
			hostConfig.Password("guest");
		});

		cfg.ConfigureEndpoints(context);
	});
});
#endregion
Console.WriteLine($"[DEBUG] DB Connection: {builder.Configuration.GetConnectionString("DbAuthConnection")}");

builder.Services.AddDbContext<AuthDbContext>(option =>
{
	string? serverDbConnectionString = builder.Configuration.GetConnectionString("DbAuthConnection");
	option.UseNpgsql(serverDbConnectionString);
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
