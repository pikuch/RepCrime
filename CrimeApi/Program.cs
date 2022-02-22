using CrimeApi.Data;
using CrimeApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICrimeEventRepository, CrimeEventRepository>();
builder.Services.AddScoped<ILawEnforcementService, LawEnforcementService>();
builder.Services.AddHttpClient();

var app = builder.Build();

CrimeEventSeeder.SeedDb(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
