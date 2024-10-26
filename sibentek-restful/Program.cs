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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=../sibentek-restful/database.db"));

builder.Services.AddScoped<UserMessageRepository>();

builder.Services.AddScoped<IUserMessageService, UserMessageService>();
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient("7410806777:AAGVoLLKhd5eqQ13nYCwWjY5o_t6fYlV9nY"));
builder.Services.AddSingleton<TelegramBot>();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var bot = scope.ServiceProvider.GetRequiredService<TelegramBot>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();