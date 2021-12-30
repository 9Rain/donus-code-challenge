
namespace DataAccess.Models
{
    public class AccountModel : BaseModel
    {
        public string Number { get; set; }
        public byte Digit { get; set; }
        public decimal Balance { get; set; }

        public long AgencyId;
        public long ClientId;
    }
}