using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher032023.Demo.DL_Repositories.Entity
{
    public class Department : BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
