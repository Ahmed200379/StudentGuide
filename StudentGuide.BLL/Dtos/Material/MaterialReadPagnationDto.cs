using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Material
{
   public class MaterialReadPagnationDto
    {
        public IEnumerable<MaterialReadDto> Materials { get; set; } = new List<MaterialReadDto>();
        public int TotalCount { get; set; }
    }
}
