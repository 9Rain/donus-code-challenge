using IBank.Dtos.Agency;

namespace IBank.Dtos.Account
{
    public class ReturnAccountDto
    {
        public ReturnAgencyDto Agency { get; set; }

        public char Number { get; set; }

        public char Digit { get; set; }
    }
}