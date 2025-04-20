using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Material
{
   public class MaterialReadDto
    {
        public int Id { get; set; }
        public String Name { get; set; } = string.Empty;
        public String Instructor { get; set; } = string.Empty;  
        public String? YoutubeLink { get; set; }
        public String? DriveLink { get; set; }
        public String? MaterialCode { get; set; }

    }
}
