using FluentValidation;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Business.CriticalPathMethod;
using ProjectPlanner.Business.TransportationProblem;
using ProjectPlanner.Business.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers().AddApplicationPart(typeof(ProjectPlanner.Application.Controllers.CpmController).Assembly);
services.AddControllers().AddApplicationPart(typeof(ProjectPlanner.Application.Controllers.TpController).Assembly);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<ICpmService, CpmService>();
services.AddScoped<IValidator<CpmTask>, CpmFluentValidator>();
services.AddScoped<ITpService, TpService>();
services.AddScoped<IValidator<TpTask>, TpFluentValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI");
        c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
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