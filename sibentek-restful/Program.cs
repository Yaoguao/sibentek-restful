using Sibentek.Core.Model;
using Sibentek.DataAccess;
using sibentek_restful;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var bot = new TelegramBot("7410806777:AAGVoLLKhd5eqQ13nYCwWjY5o_t6fYlV9nY");

using (var context = new ApplicationDbContext())
{
    context.Database.EnsureCreated();
    
    // Создание нового пользователя
    User user = new User {Name = "Ancherm"};
    context.Users.Add(user);
    context.SaveChanges();
    
    // Чтение пользователей
    var users = context.Users.ToList();
    Console.WriteLine(string.Join(", ", users));

    // Обновление пользователя
    var firstUser = context.Users.First();
    firstUser.Name = "Bob";
    context.SaveChanges();
    
    // Удаление пользователя
    context.Users.Remove(firstUser);
    context.SaveChanges();
}

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