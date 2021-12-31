
namespace DataAccess.Models
{
    public class AccountModel : BaseModel
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public byte Digit { get; set; }

        public string ShortPassword { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }


        public int AgencyId { get; set; }

        public long ClientId { get; set; }
    }
}