using Challenge;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddControllers();

builder.Services.AddDbContext<ChallengeContext>();

//builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

ApplyMigrations(app);

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run("http://localhost:5000");

static void ApplyMigrations(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ChallengeContext>();
    db.Database.Migrate();
}