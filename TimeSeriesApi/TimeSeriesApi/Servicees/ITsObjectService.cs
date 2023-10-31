using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSeriesApi.Models;
using TimeSeriesApi.Repositories;

namespace TimeSeriesApi.Servicees
{
    public interface ITsObjectService : IGenericRepository<TsObject>
    {
        Task<IEnumerable<TsObject>> GetListAsync();
    }
}
