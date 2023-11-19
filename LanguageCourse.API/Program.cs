using LanguageCourse.Application.Services;
using LanguageCourse.Infrastructure.Data;
using LanguageCourse.Infrastructure.Repositories;
using LanguageCourse.Domain.Repositories;
using LanguageCourse.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMemoryLanguageCourse"));
}
builder.Services.AddScoped<StudentService>();

builder.Services.AddScoped<IRepository<Student>, StudentRepository>();

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
