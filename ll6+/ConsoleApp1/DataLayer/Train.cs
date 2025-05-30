namespace RailwayTransport.Core.Entities
{
    public class Train
    {
        public int TrainId { get; set; }
        public string TrainNumber { get; set; }
        public string TrainType { get; set; }
        public string DeparturePoint { get; set; }
        public string DestinationPoint { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}