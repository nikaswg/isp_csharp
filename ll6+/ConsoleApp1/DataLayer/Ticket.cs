namespace RailwayTransport.Core.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string PassengerName { get; set; }
        public int TrainId { get; set; }
        public int CarriageId { get; set; }
        public int SeatNumber { get; set; }
        public DateTime SaleDate { get; set; }

        // Additional properties for display
        public string TrainNumber { get; set; }
        public string CarriageNumber { get; set; }
        public decimal SeatPrice { get; set; }
    }
}