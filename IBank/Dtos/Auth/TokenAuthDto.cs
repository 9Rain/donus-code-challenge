using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBank.Dtos.Auth
{
    public class TokenAuthDto
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
    }
}
