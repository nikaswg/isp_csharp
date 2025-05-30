namespace RailwayTransport.Core.Entities
{
    public class Carriage
    {
        public int CarriageId { get; set; }
        public string CarriageNumber { get; set; }
        public string CarriageType { get; set; }
        public int SeatCount { get; set; }
        public decimal SeatPrice { get; set; }
        public int TrainId { get; set; }
    }
}