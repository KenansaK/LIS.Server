using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Dtos
{
    public class PermissionDto
    {
        public long Id { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public string Module { get; set; }
    }
}
