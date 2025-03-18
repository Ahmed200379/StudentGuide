using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Material
{
  public  class MaterialAddDto
    {
        public String Name { get; set; } = string.Empty;
        public String Instructor { get; set; } = string.Empty;
        public String? Drive { get; set; } = string.Empty;
        public String? Youtube { get; set; } = string.Empty;
        public String? Code { get; set; }
    }
}
