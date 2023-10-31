using System;

namespace TimeSeriesApi.Models
{
    public class Reading
    {
        public short BuildingId { get; set; }
        public byte ObjectId { get; set; }
        public byte DatafieldId { get; set; }
        public decimal Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
