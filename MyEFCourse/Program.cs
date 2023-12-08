using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyEFCourse.Entities;
using System.Text.Json.Serialization;
using System.Threading.Tasks.Dataflow;


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
    (option => option
    .UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnStr"))
    .UseLazyLoadingProxies()
    ) ;

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
app.MapGet("data", async (MyBoardsContext db) =>
    {
    var withAddress = true;
    var user = db.Users
    .First(u => u.Id == Guid.Parse("1ECC80FA-8CA2-4291-AC61-08DBF68C0831"));
        if (withAddress)
        {
            var result = new { FirstName = user.FirstName, Address = $"{user.Address.Street} {user.Address.City}" };
            return result;
        }

        return new { FirstName = user.FirstName, Address = "-" };

        // Added MS EntityFramework Proxies
    });
app.Run();

