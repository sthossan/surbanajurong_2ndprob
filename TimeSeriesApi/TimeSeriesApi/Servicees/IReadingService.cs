using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TimeSeriesApi.Models;
using TimeSeriesApi.Repositories;
using TimeSeriesApi.ViewModels;

namespace TimeSeriesApi.Servicees
{
    public interface IReadingService: IGenericRepository<Reading>
    {
        Task<List<ReadingViweModel>> GetListAsync(short buildingId, byte objectId, byte datafieldId, DateTime startDateTime, DateTime endDateTime);

        Task<List<ReadingViweModel>> GetListBySpAsync(short buildingId, byte objectId, byte datafieldId, DateTime startDateTime, DateTime endDateTime);
    }
}
