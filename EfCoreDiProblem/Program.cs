using System.Reflection;
using EfCoreDiProblem.Context;
using EfCoreDiProblem.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<TestDbContext>(options =>
    {
        options.UseInMemoryDatabase("TestDb");
        options.EnableSensitiveDataLogging();
    }
);

services.AddScoped<ITestService, TestService>();

services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

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

