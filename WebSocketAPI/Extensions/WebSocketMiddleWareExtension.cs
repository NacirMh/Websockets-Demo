using WebSocketAPI.MiddleWares;

namespace WebSocketAPI.Extensions
{
    public static class WebSocketMiddleWareExtension
    {
        public static IApplicationBuilder UseWebSocketMiddleWare(this IApplicationBuilder app) { 
        
              return app.UseMiddleware<WebSocketsMiddleWare>();
        }
    }
}
