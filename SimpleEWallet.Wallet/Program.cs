using MassTransit;

using Microsoft.EntityFrameworkCore;

using SimpleEWallet.Comon.MQ;
using SimpleEWallet.Wallet.Consumers;
using SimpleEWallet.Wallet.Persistence;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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

	regisConfig.UsingRabbitMq((context, cfg) =>
	{
		// default ke host localhost
		//cfg.Host("localhost", "/", hostConfig =>
		//{
		//	hostConfig.Username("admin");
		//	hostConfig.Password("admin");
		//});

		cfg.ReceiveEndpoint(MQQueueNames.Wallet.InitializeWallet, endpoint =>
		{
			endpoint.Durable = true;
			endpoint.UseMessageRetry(ret => ret.Interval(20, 10));
			endpoint.ConfigureConsumer<InitializeWalletConsumer>(context);
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
