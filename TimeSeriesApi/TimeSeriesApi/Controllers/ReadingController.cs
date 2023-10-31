using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TimeSeriesApi.Servicees;

namespace TimeSeriesApi.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class ReadingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;
        private readonly ITsObjectService _tsObjectService;
        private readonly IDataFieldService _dataFieldService;

        private readonly IReadingService _readingService;


        public ReadingController(IBuildingService buildingService, ITsObjectService tsObjectService, IDataFieldService dataFieldService, IReadingService readingService)
        {
            _buildingService = buildingService;
            _tsObjectService = tsObjectService;
            _dataFieldService = dataFieldService;
            _readingService = readingService;
        }

        [HttpGet("getbuildingddl")]
        public async Task<IActionResult> GetBuildingDdl()
        {
            try
            {
                return Ok(await _buildingService.GetListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getobjectddl")]
        public async Task<IActionResult> GetObjectgDdl()
        {
            try
            {
                return Ok(await _tsObjectService.GetListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getDataFieldddl")]
        public async Task<IActionResult> GetDataFieldDdl()
        {
            try
            {
                return Ok(await _dataFieldService.GetListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getreadinglist")]
        public async Task<IActionResult> GetReadingList(short buildingId, byte objectId, byte datafieldId, DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                return Ok(await _readingService.GetListAsync(buildingId, objectId, datafieldId, startDateTime, endDateTime));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getreadingsplist")]
        public async Task<IActionResult> GetReadingSpList(short buildingId, byte objectId, byte datafieldId, DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                return Ok(await _readingService.GetListBySpAsync(buildingId, objectId, datafieldId, startDateTime, endDateTime));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
