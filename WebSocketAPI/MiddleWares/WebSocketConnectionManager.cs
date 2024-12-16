using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace WebSocketAPI.MiddleWares
{
    public class WebSocketConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public ConcurrentDictionary<string, WebSocket> getSockets()
        {
            return _sockets;
        }
        public string AddSocket(WebSocket ws)
        {
            string conId = Guid.NewGuid().ToString();   
            _sockets.TryAdd(conId, ws); 
            return conId;   
        }
    }
}
