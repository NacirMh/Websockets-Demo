using System.Net.Sockets;
using System.Net.WebSockets;
using WebSocketAPI.Extensions;
using WebSocketAPI.Interfaces;
using WebSocketAPI.MiddleWares;
using WebSocketAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WebSocketConnectionManager>();
builder.Services.AddSingleton<ITokenManager, TokenManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseWebSockets();
app.UseWebSocketMiddleWare();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
