using Models.TimeSeriesApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TimeSeriesApi.Models;
using TimeSeriesApi.Repositories;

namespace TimeSeriesApi.Servicees
{
    public class TsObjectService : GenericRepository<TsObject>, ITsObjectService
    {
        public TsObjectService(CustomDbContext context) : base(context) { }

        public async Task<IEnumerable<TsObject>> GetListAsync()
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
