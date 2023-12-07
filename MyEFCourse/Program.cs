using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyEFCourse.Entities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<MyBoardsContext>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnStr"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<MyBoardsContext>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations != null)
{
    dbContext.Database.Migrate();
}

var users = dbContext.Users.ToList();
if (!users.Any())
{
    var ExUser1 = new User()
    {
        Email = "userwow1@us.os",
        FirstName = "Gee",
        LastName = "Roo",
        Address = new Address()
        {
            City = "Bro City",
            Street = "Grove Street"
        }
    };

    var ExUser2 = new User()
    {
        Email = "userwow2@us.as",
        FirstName = "Heere",
        LastName = "troo",
        Address = new Address()
        {
            City = "Bundle City",
            Street = "GHK Street"
        }
    };

    dbContext.Users.AddRange(ExUser1, ExUser2);
    dbContext.SaveChanges();
}

// Example endpoiny query
app.MapDelete("data", async (MyBoardsContext db) =>
    {
        var workitemTags = await db.WorkItemTags.Where(c => c.WorkItemId == 1).ToListAsync();
        db.WorkItemTags.RemoveRange(workitemTags);

        var workItem = await db.WorkItems.FirstAsync(c => c.Id == 2);
        db.RemoveRange(workitemTags);

        await db.SaveChangesAsync();
    });
app.Run();

