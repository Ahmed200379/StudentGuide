using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Document
{
   public class DocumentReadPagnationDto
    {
        public IEnumerable<DocumentReadDto> Documents { get; set; } = new List<DocumentReadDto>();
        public int TotalCount { get; set; }
    }
}
