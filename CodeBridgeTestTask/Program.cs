using CodeBridgeTestTask.Data;
using CodeBridgeTestTask.IServices;
using CodeBridgeTestTask.RateLimitingMiddleware;
using CodeBridgeTestTask.Services;
using DAL.Interfaces.IRepositories;
using DAL.Interfaces.IUnitOfWork;
using DAL.Repositories;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DogDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDogRepository, DogRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDogService, DogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimitingMiddleware(maxRequests: 10, timeSpan: TimeSpan.FromSeconds(1));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
