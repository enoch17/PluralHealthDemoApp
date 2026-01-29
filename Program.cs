using Microsoft.Extensions.Options;
using PluralHealthDemoApp.Services;
using Serilog;
using System.Text.Json.Serialization;

//Configuring Logger
Log.Logger = new LoggerConfiguration()
   .WriteTo.File(
       "Logs/app-.log",
       rollingInterval: RollingInterval.Day,
       retainedFileCountLimit: 14,
       outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"
   )
   .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//My Services
builder.Services.AddSingleton<AppointmentService>();
builder.Services.AddScoped<AppointmentStatusFlowService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<UserContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
