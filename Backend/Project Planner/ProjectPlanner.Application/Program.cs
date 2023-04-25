using FluentValidation;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Business.CriticalPathMethod.Dtos;
using ProjectPlanner.Business.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IValidator<CpmTask>, CpmFluentValidator>();
services.AddScoped<ICpmService, CpmService>();
services.AddScoped<ITpService, TpService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .AllowAnyHeader()
    .WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();