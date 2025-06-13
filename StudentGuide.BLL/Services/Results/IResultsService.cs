using StudentGuide.BLL.Dtos.Account;
using StudentGuide.BLL.Dtos.Result;
using StudentGuide.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.Results
{
   public interface IResultsService
    {
        public Task<MessageResponseDto> DeleteCourse(ResultDeleteDto reseltDeleteDto);
        public Task AddResult(IEnumerable<ResultAddDto> results);
        public Task<IEnumerable<ResultReadForAdminDto>> GetAllResultForAdmin(ResultReadForResult specificUser);
        public Task<IEnumerable<ResultReadForStudentDto>> GetAllResultForSpecificStudent(ResultReadForResult specificUser);
        public Task<IEnumerable<ResultsReadForAllStudents>> GetAllResultsForAllStudents(string semester);

        public Task AddResultWithExcel(Stream results);
    }
}
