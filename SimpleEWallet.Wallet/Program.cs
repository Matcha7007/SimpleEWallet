using MassTransit;

using Microsoft.EntityFrameworkCore;

using SimpleEWallet.Comon.MQ;
using SimpleEWallet.Wallet.Consumers;
using SimpleEWallet.Wallet.Persistence;

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
	regisConfig.AddConsumersFromNamespaceContaining<InitializeWalletConsumer>();
	regisConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));

	regisConfig.AddConsumersFromNamespaceContaining<UpdateStatusTopupConsumer>();
	regisConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));

	regisConfig.AddConsumersFromNamespaceContaining<UpdateStatusTransferConsumer>();
	regisConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));

	regisConfig.AddConsumersFromNamespaceContaining<UpdateTransactionIdConsumer>();
	regisConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));

	regisConfig.AddConsumersFromNamespaceContaining<DeleteDataWalletConsumer>();
	regisConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));

	regisConfig.UsingRabbitMq((context, cfg) =>
	{
		// default ke host localhost
		cfg.Host("rabbitmq_wallet", "/", hostConfig =>
		{
			hostConfig.Username("guest");
			hostConfig.Password("guest");
		});

		cfg.ReceiveEndpoint(MQQueueNames.Wallet.InitializeWallet, endpoint =>
		{
			endpoint.Durable = true;
			endpoint.UseMessageRetry(ret => ret.Interval(20, 10));
			endpoint.ConfigureConsumer<InitializeWalletConsumer>(context);
		});

		cfg.ReceiveEndpoint(MQQueueNames.Wallet.UpdateStatusTopup, endpoint =>
		{
			endpoint.Durable = true;
			endpoint.UseMessageRetry(ret => ret.Interval(20, 10));
			endpoint.ConfigureConsumer<UpdateStatusTopupConsumer>(context);
		});

		cfg.ReceiveEndpoint(MQQueueNames.Wallet.UpdateStatusTransfer, endpoint =>
		{
			endpoint.Durable = true;
			endpoint.UseMessageRetry(ret => ret.Interval(20, 10));
			endpoint.ConfigureConsumer<UpdateStatusTransferConsumer>(context);
		});

		cfg.ReceiveEndpoint(MQQueueNames.Wallet.UpdateTransactionId, endpoint =>
		{
			endpoint.Durable = true;
			endpoint.UseMessageRetry(ret => ret.Interval(20, 10));
			endpoint.ConfigureConsumer<UpdateTransactionIdConsumer>(context);
		});

		cfg.ReceiveEndpoint(MQQueueNames.Wallet.DeleteDataWallet, endpoint =>
		{
			endpoint.Durable = true;
			endpoint.UseMessageRetry(ret => ret.Interval(20, 10));
			endpoint.ConfigureConsumer<DeleteDataWalletConsumer>(context);
		});

		cfg.ConfigureEndpoints(context);
	});
});
#endregion

builder.Services.AddDbContext<WalletDbContext>(option =>
{
	string? serverDbConnectionString = builder.Configuration.GetConnectionString("DbWalletConnection");
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
