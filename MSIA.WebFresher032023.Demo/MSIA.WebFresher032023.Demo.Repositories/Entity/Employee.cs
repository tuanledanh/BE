using MSIA.WebFresher032023.Demo.Commons.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher032023.Demo.DL_Repositories.Entity
{
    public class Employee : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
