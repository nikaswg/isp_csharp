namespace Lunopark.Core.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? BirthYear { get; set; }
        public string Position { get; set; }
    }
}