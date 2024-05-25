using AutoMapper.Configuration.Annotations;
using DnD.Archive.Api.Helpers.Automapper;
using DnD.Archive.Api.Helpers.DB;
using DnD.Archive.Api.Models;
using DnD.Archive.Api.Services.Abstract;
using DnD.Archive.Api.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(_ =>
{
    var dbContextFactory = new DnDArchiveContextFactory();
    return dbContextFactory.CreateDbContext(args);
});

builder.Services.AddScoped<ICharacterService, CharacterService >();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<ISkillService, SkillService>();

builder.Services.AddAutoMapper(typeof(DnDArchiveMappingProfile));

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


app.Seed();
app.Run();
