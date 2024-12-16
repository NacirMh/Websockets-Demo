namespace WebSocketAPI.Interfaces
{
    public interface ITokenManager
    {
        public Token NewToken();
        public bool Authenticate(string name, string password);
        public bool verifyToken(string token);

    }
}
