
namespace DataAccess.Models
{
    public class AgencyModel : BaseModel
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public byte Digit { get; set; }
    }
}