using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MyEFCourse.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
app.MapPost("update", async (MyBoardsContext db) =>
    {
        User userupdate = await db.Users.FirstAsync( userupdate => userupdate.FirstName == "Gee");

        userupdate.LastName = "UpdatedLastName";
    });
app.Run();

