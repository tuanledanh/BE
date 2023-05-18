using MSIA.WebFresher032023.ConnectToDB.Repositories;
using System.Text.Json;
using AutoMapper;
using MSIA.WebFresher032023.ConnectToDB.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add json options to convert value to pascal case
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<EmployeeRepository>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
