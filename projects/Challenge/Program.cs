using Challenge;
using Challenge.Services.Config;
using Microsoft.EntityFrameworkCore;
using Challenge.Interfaces;
using Challenge.Repositories;
using Challenge.Seeds;
using System;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddHttpClient();

services.AddControllers();

//services cors
services.AddCors();
// global cors policy


services.AddSingleton<IEnvironenmentConfigs, EnvironmentConfigs>();

// adds repositories
services.AddScoped<IContactsRepository, ContactsRepository>();
services.AddScoped<IGroupsRepository, GroupsRepository>();

services.AddDbContext<ChallengeContext>();

// adds seeds
services.AddTransient<ContactsSeeder>();
services.AddTransient<PhoneNumbersSeeder>();
services.AddTransient<GroupsSeeder>();
services.AddTransient<ContactGroupsSeeder>();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

while (true)
{
    try
    {
        Console.WriteLine("Trying to run migrations");
        ApplyMigrations(app);
        Console.WriteLine("Migrations Successfully ran");
        Console.WriteLine("Trying to run seeds");
        SeedData(app);
        Console.WriteLine("Seeds Successfully ran");
        break;
    }
    catch
    {
        Console.WriteLine("Could no apply migrations or seeds");
        Console.WriteLine("Waiting 2 seconds.............");
        System.Threading.Thread.Sleep(2000);
    }
}


// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.MapControllers();

app.Run("http://0.0.0.0:5000");
//app.Run("http://localhost:5000");

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