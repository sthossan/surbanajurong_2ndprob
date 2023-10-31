using Microsoft.EntityFrameworkCore;
using Models.TimeSeriesApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TimeSeriesApi.Extension;
using TimeSeriesApi.Models;
using TimeSeriesApi.Repositories;
using TimeSeriesApi.ViewModels;

namespace TimeSeriesApi.Servicees
{
    public class ReadingService : GenericRepository<Reading>, IReadingService
    {
        private readonly CustomDbContext _customDbContext;
        public ReadingService(CustomDbContext context) : base(context)
        {
            _customDbContext = context;
        }

        public async Task<List<ReadingViweModel>> GetListAsync(short buildingId, byte objectId, byte datafieldId, DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                if (buildingId == 0)
                    throw new ArgumentNullException(nameof(buildingId));

                Expression<Func<Reading, bool>> expression = t => t.BuildingId == buildingId;
                if (objectId == 0 && datafieldId == 0)
                    expression = t => t.BuildingId == buildingId && t.Timestamp >= startDateTime && t.Timestamp <= endDateTime;
                else if (objectId == 0)
                    expression = t => t.BuildingId == buildingId && t.DatafieldId == datafieldId && t.Timestamp >= startDateTime && t.Timestamp <= endDateTime;
                else if (datafieldId == 0)
                    expression = t => t.BuildingId == buildingId && t.ObjectId == objectId && t.Timestamp >= startDateTime && t.Timestamp <= endDateTime;
                else
                    expression = t => t.BuildingId == buildingId && t.ObjectId == objectId && t.DatafieldId == datafieldId && t.Timestamp >= startDateTime && t.Timestamp <= endDateTime;



                return await GetAllAsync(expression)
                                .Select(sm => new ReadingViweModel
                                {
                                    Value = sm.Value,
                                    Timestamp = sm.Timestamp
                                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ReadingViweModel>> GetListBySpAsync(short buildingId, byte objectId, byte datafieldId, DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                if (buildingId == 0)
                    throw new ArgumentNullException(nameof(buildingId));

                return await _customDbContext.LoadStoredProc("GetReadingData")
                                        .WithSqlParam("buildingId", buildingId)
                                        .WithSqlParam("objectId", objectId)
                                        .WithSqlParam("datafieldId", datafieldId)
                                        .WithSqlParam("startDateTime", startDateTime)
                                        .WithSqlParam("endDateTime", endDateTime)
                                        .ExecuteStoredProcAsync<ReadingViweModel>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
