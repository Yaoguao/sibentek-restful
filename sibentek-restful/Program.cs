using Microsoft.EntityFrameworkCore;
using ModelTextForApi;
using Sibentek.Core.Model;
using Sibentek.DataAccess;
using sibentek_restful;
using Sibentek.Application.Service;
using Sibentek.Core.Interface;
using Sibentek.DataAccess.repositories;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=../sibentek-restful/database.db"));

builder.Services.AddScoped<UserMessageRepository>();

builder.Services.AddScoped<IUserMessageService, UserMessageService>();
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient("7410806777:AAGVoLLKhd5eqQ13nYCwWjY5o_t6fYlV9nY"));
builder.Services.AddSingleton<TelegramBot>();
builder.Services.AddSwaggerGen();


/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=../sibentek-restful/database.db"));
*/


var app = builder.Build();
    //var bot = new TelegramBot("7410806777:AAGVoLLKhd5eqQ13nYCwWjY5o_t6fYlV9nY");
using var scope = app.Services.CreateScope();
var bot = scope.ServiceProvider.GetRequiredService<TelegramBot>();

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