using Models.TimeSeriesApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TimeSeriesApi.Models;
using TimeSeriesApi.Repositories;

namespace TimeSeriesApi.Servicees
{
    public class DataFieldService : GenericRepository<DataField>, IDataFieldService
    {
        public DataFieldService(CustomDbContext context) : base(context) { }

        public async Task<IEnumerable<DataField>> GetListAsync()
        {
            try
            {
                return await GetAllAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
