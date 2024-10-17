using Application;
using Carter;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();
app.Run();
