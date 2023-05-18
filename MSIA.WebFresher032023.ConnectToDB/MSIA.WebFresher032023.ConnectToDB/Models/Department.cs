namespace MSIA.WebFresher032023.ConnectToDB.Models
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set;}
    }
}
