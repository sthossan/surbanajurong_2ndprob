using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSeriesApi.Models;
using TimeSeriesApi.Repositories;

namespace TimeSeriesApi.Servicees
{
    public interface IBuildingService : IGenericRepository<Building>
    {
        Task<IEnumerable<Building>> GetListAsync();
    }
}
