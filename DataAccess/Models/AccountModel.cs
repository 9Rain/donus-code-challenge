
namespace DataAccess.Models
{
    public class AccountModel : BaseModel
    {
        public AccountModel()
        {
        }

        public AccountModel(
            AgencyModel agency, string number, byte digit, string shortPassword, 
            string password, long clientId
        )
        {
            Agency = agency;
            Number = number;
            Digit = digit;
            ShortPassword = shortPassword;
            Password = password;
            ClientId = clientId;
        }

        public long Id { get; set; }
        public AgencyModel Agency { get; set; }
        public string Number { get; set; }
        public byte Digit { get; set; }
        public string ShortPassword { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public long ClientId { get; set; }
    }
}