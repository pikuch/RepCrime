using CrimeFeedbackService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHostedService<RabbitReceiver>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
