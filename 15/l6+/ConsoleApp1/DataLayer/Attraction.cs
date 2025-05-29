namespace Lunopark.Core.Entities
{
    public class Attraction
    {
        public int AttractionId { get; set; }
        public string AttractionName { get; set; }
        public int InstallationYear { get; set; }
        public int? ResponsibleEmployeeId { get; set; }
    }
}