using IBank.Dtos.Agency;

namespace IBank.Dtos.Account
{
    public class ReturnAccountDto
    {
        public ReturnAgencyDto Agency { get; set; }
        public string Number { get; set; }
        public string Digit { get; set; }
    }
}