using DnD.Archive.Api.Helpers.Automapper;
using DnD.Archive.Api.Helpers.DB;
using DnD.Archive.Api.Services.Abstract;
using DnD.Archive.Api.Services.Implementation;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

const string DB_CONNECTION_VARIABLE_PATH = "ConnectionStrings:SQLCONNSTR_DnDArchive";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(_ =>
{
    var connString = configuration[DB_CONNECTION_VARIABLE_PATH];
    var dbContextFactory = new DnDArchiveContextFactory(connString);
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
