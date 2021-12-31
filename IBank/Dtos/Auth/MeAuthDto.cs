using IBank.Dtos.Account;

namespace IBank.Dtos.Auth
{
    public class MeAuthDto
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public ReturnAccountDto Account { get; set; }
    }
}
