namespace Lunopark.Core.Entities
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public string TicketNumber { get; set; }
        public int AttractionId { get; set; }
        public decimal TicketPrice { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime SaleDate { get; set; }
    }
}