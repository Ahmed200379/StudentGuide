using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Result
{
   public class ResultsReadForAllStudents
    {
       public IEnumerable<ResultReadForAdminDto> Results {  get; set; }= new List<ResultReadForAdminDto>();
       public int TotalCount { get; set; }

    }
}
