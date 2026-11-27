using Hangfire;
using Hangfire.Dashboard;
using HangfireBasicAuthenticationFilter;
using Microsoft.OpenApi;
using Serilog;
using SurveyBasket.Application.Interfaces.NotificationService;
using SurveyBasket.Infrastructure.Extensions;
using SurveyBasket.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
                 .MinimumLevel.Information()
                 .WriteTo.Console();
});

builder.Services.AddApplicationServices();

builder.Services.AddWebApiServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseHangfireDashboard();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard("/Jops", new DashboardOptions
{
    Authorization =
    [
        new HangfireCustomBasicAuthenticationFilter
        {
            User = app.Configuration.GetValue<string>("HangfireSettings:User"),
            Pass = app.Configuration.GetValue<string>("HangfireSettings:Pass")
        }
    ],
    DashboardTitle = " Suervey Basket Dashboard ",
    //IsReadOnlyFunc = (DashboardContext context)=>true
});

var scopeFactory = app.Services.GetRequiredService<IServiceProvider>();
using var scope = scopeFactory.CreateAsyncScope();
var notificationService = scope.ServiceProvider.GetRequiredService<IPollNotificationService>();

RecurringJob.AddOrUpdate(
    "NewPoll",
    () => notificationService.SendNewPollNotificationAsync(null),
    Cron.Daily);


app.MapControllers();

app.UseExceptionHandler();

app.Run();
