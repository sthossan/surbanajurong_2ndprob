using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSeriesApi.Models;
using TimeSeriesApi.Repositories;

namespace TimeSeriesApi.Servicees
{
    public interface IDataFieldService : IGenericRepository<DataField>
    {
        Task<IEnumerable<DataField>> GetListAsync();
    }
}
