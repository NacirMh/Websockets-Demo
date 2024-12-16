using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebSocketAPI.Domain;

namespace WebSocketAPI.MiddleWares
{
    public class WebSocketsMiddleWare
    {
        public readonly RequestDelegate _next;
        public readonly WebSocketConnectionManager _webSocketManager;
        public WebSocketsMiddleWare(RequestDelegate next , WebSocketConnectionManager webSocketManager)
        {
            _next = next;
            _webSocketManager = webSocketManager;   
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                string conID = _webSocketManager.AddSocket(webSocket);
                await SendMessageAsync(webSocket, conID);
                Console.WriteLine("socket connected...");
                
                await ReceiveMessageAsync(webSocket, async (result, message) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine("Message received...");
                        string msgRecu = Encoding.UTF8.GetString(message,0,result.Count);
                        Console.WriteLine($"message : {msgRecu}");
                        await RouteJSONMessagesAsync(msgRecu);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Receive Closed...");
                        return;

                    }
                });
            }
            else
            {
                await _next(context);
            }
        }

        private async Task ReceiveMessageAsync(WebSocket ws, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), CancellationToken.None);
                handleMessage(result, buffer);
            }
        }

        private async Task SendMessageAsync(WebSocket ws , string id)
        {
            if (ws.State == WebSocketState.Open)
            {
                await ws.SendAsync(Encoding.UTF8.GetBytes(id), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task RouteJSONMessagesAsync(string message)
        {
            var data = JsonSerializer.Deserialize<MessageTransmis>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
            if(Guid.TryParse(data.To, out var guid))
            {
                WebSocket ws = _webSocketManager.getSockets().First(x => x.Key == data.To).Value;
                if(ws.State == WebSocketState.Open)
                {
                    await SendMessageAsync(ws, data.Message);
                }
            }
        }
    }

    


}
