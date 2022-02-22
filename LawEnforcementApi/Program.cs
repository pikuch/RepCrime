using LawEnforcementApi.Data;
using LawEnforcementApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LawEnforcementDbContext>(options =>
                options.UseInMemoryDatabase("LawEnforcementDb"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ILawEnforcementRepository, LawEnforcementRepository>();

var app = builder.Build();

LawEnforcementDbContextSeeder.SeedDb(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
