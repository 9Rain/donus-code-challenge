namespace DataAccess.Models
{
    public class ClientModel : BaseModel
    {
        public ClientModel()
        {
        }

        public ClientModel(string name, string cpf)
        {
            Name = name;
            Cpf = cpf;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public AccountModel Account { get; set; }
    }
}
