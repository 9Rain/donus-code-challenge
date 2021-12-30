
namespace DataAccess.Models
{
    public class AccountModel : BaseModel
    {
        public string number { get; set; }
        public byte digit { get; set; }
        public decimal balance { get; set; }

        public long AgencyId;
        public long ClientId;
    }
}