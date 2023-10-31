using Models.TimeSeriesApi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSeriesApi.Models;
using TimeSeriesApi.Repositories;

namespace TimeSeriesApi.Servicees
{
    public class BuildingService : GenericRepository<Building>, IBuildingService
    {
        public BuildingService(CustomDbContext context) : base(context) { }

        public async Task<IEnumerable<Building>> GetListAsync()
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
