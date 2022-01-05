using DataAccess.Connection;
using DataAccess.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data.Agency
{
    public class AgencyData : DataManager, IAgencyData
    {
        public AgencyData(IConnection connection): base(connection) { }

        public async Task<AgencyModel> Get(int id)
        {
            var result = await this.LoadData<AgencyModel, dynamic>(
                "dbo.spAgency_Get", new { Id = id });

            return result.FirstOrDefault();
        }
    }
}
