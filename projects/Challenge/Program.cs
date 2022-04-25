using Challenge;
using Challenge.Services.Config;
using Microsoft.EntityFrameworkCore;
using Challenge.Interfaces;
using Challenge.Repositories;
using Challenge.Seeds;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddHttpClient();

services.AddControllers();

//services cors
services.AddCors();
// global cors policy


services.AddSingleton<IEnvironenmentConfigs, EnvironmentConfigs>();

//
services.AddScoped<IContactsRepository, ContactsRepository>();
services.AddScoped<IGroupsRepository, GroupsRepository>();

services.AddDbContext<ChallengeContext>();

//
services.AddTransient<ContactsSeeder>();
services.AddTransient<PhoneNumbersSeeder>();
services.AddTransient<GroupsSeeder>();
services.AddTransient<ContactGroupsSeeder>();

//builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

ApplyMigrations(app);
SeedData(app);

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

//app.Run("http://0.0.0.0:5000");
app.Run("http://localhost:5000");

static void ApplyMigrations(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ChallengeContext>();
    db.Database.Migrate();
}

static void SeedData(WebApplication app)
{
    var scope = app.Services.CreateScope();

    var contactsSeeder = scope.ServiceProvider.GetService<ContactsSeeder>();
    contactsSeeder.Seed();

    var phoneNumbersSeeder = scope.ServiceProvider.GetService<PhoneNumbersSeeder>();
    phoneNumbersSeeder.Seed();

    var groupsSeeder = scope.ServiceProvider.GetService<GroupsSeeder>();
    groupsSeeder.Seed();

    var contactsGroupSeeder = scope.ServiceProvider.GetService<ContactGroupsSeeder>();
    contactsGroupSeeder.Seed();
}