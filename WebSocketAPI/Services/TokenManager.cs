using WebSocketAPI.Interfaces;

namespace WebSocketAPI.Services
{
    public class TokenManager : ITokenManager
    {
        List<Token> _tokens;
        public TokenManager() { 
            _tokens = new List<Token>();
        }
        public bool Authenticate(string name, string password)
        {
            if(name == "Admin" && password == "Admin")
            {
                return true;
            }
            return false;
        }

        public Token NewToken()
        {
            var token =  new Token()
            {
                Value = Guid.NewGuid().ToString(),
                ExpireDate = DateTime.Now.AddMinutes(20),
            };
            _tokens.Add(token);
            return token;
        }

        public bool verifyToken(string token)
        {
            var t = _tokens.FirstOrDefault(x => x.Value == token);
            if (t != null && t.ExpireDate > DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
