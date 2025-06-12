using StudentGuide.BLL.Dtos.Account;
using StudentGuide.BLL.Dtos.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.Results
{
   public interface IResultsService
    {
        public Task<MessageResponseDto> DeleteCourse(ResultDeleteDto reseltDeleteDto);
        public Task AddResult(IEnumerable<ResultAddDto> results);
        public Task<IEnumerable<ResultReadForAdminDto>> GetAllResultForAdmin(String code, string semester);
        public Task<IEnumerable<ResultReadForStudentDto>> GetAllResultForSpecificStudent(String code, string semester);
        public Task<IEnumerable<ResultsReadForAllStudents>> GetAllResultsForAllStudents(string semester);
        public Task AddResultWithExcel(Stream results);

    }
}
