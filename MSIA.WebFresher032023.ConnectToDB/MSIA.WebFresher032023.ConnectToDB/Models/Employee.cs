using MSIA.WebFresher032023.ConnectToDB.Enum;
using System.Reflection;

namespace MSIA.WebFresher032023.ConnectToDB.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
