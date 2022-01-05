using DataAccess.Models;
using System.Threading.Tasks;

namespace DataAccess.Data.Agency
{
    public interface IAgencyData
    {
        Task<AgencyModel> Get(int id);
    }
}
