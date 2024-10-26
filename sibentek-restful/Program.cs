using ModelTextForApi;
using sibentek_restful;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Logging
// TODO - убрать как закончим
var sampleData = new MLModel1.ModelInput()
{
    Topic = @"I cant connect internet",
};
var res = MLModel1.Predict(sampleData);

Console.WriteLine("Log ml: " + res.PredictedLabel);
Console.WriteLine("Log ml: " + res.Score[0] * 100);
// log

var app = builder.Build();
var bot = new TelegramBot("7410806777:AAGVoLLKhd5eqQ13nYCwWjY5o_t6fYlV9nY");

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