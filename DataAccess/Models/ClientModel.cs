namespace DataAccess.Models
{
    public class ClientModel : BaseModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }
    }
}
