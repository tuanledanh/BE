using MSIA.WebFresher032023.Demo.Commons.Enum;

namespace MSIA.WebFresher032023.Demo.BL_Services.DTO.Empl
{
    public class EmployeeDto
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public Guid DepartmentId { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string CreatedBy { get; set; }
    }
}
