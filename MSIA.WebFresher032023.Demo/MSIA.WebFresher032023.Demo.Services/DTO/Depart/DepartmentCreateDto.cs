using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher032023.Demo.BL_Services.DTO.Depart
{
    public class DepartmentCreateDto
    {
        public string DepartmentName { get; set; }
        public string? CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
