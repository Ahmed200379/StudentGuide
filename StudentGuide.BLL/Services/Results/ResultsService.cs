using OfficeOpenXml;
using StudentGuide.API.Helpers;
using StudentGuide.BLL.Constant;
using StudentGuide.BLL.Dtos.Result;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
namespace StudentGuide.BLL.Services.Results
{
    public class ResultsService : IResultsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelper _helper;
        public ResultsService(IUnitOfWork unitOfWork,IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }
        public async Task AddResult(IEnumerable<ResultAddDto> results)
        {
            if (results == null || !results.Any())
                throw new Exception(Exceptions.ExceptionMessages.GetAddFailedMessage("Results"));
            var student = await _unitOfWork.StudentRepo.GetByIdAsync(results.First().StudentId);
            if (student == null)
                throw new Exception(Exceptions.ExceptionMessages.GetNotFoundMessage("Student"));
            var resultsOfStudent = results.Select(r=>
                new StudentCourse
                {
                    CourseCode=r.CourseId,
                    StudentId=r.StudentId,
                    Grade=r.GradeOfFinal+r.GradeWithoutFinal,
                    IsPassed=(r.GradeOfFinal>=15 && (r.GradeOfFinal+r.GradeWithoutFinal)>=50)
                }).ToList();
             _unitOfWork.ResultRepo.UpdateRangeAsync(resultsOfStudent);
            int isAdded = await _unitOfWork.Complete();
            if (isAdded == 0)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetAddFailedMessage("Results"));
            }
            var Hours = resultsOfStudent.Where(c => c.IsPassed).Select(c=>c.Course.Hours).Sum();
            int currentSemester = ConstantData.SemestersByName[student.Semester];
            if (currentSemester%2==0)
            {
               currentSemester= _helper.GoToNextSemester(Hours, currentSemester);
            }
            else
            {
                currentSemester++;
            }
            var allPassedCourses=await _unitOfWork.ResultRepo.GetAllAsync(c=>c.StudentId==student.Code && c.IsPassed);
            var gpa = _helper.CalculateGPA(allPassedCourses);
            student.Semester = ConstantData.SemestersByNumber[currentSemester];
            student.Gpa = gpa;
            await _unitOfWork.StudentRepo.Update(student);
            int isUpdated = await _unitOfWork.Complete();
            if (isAdded == 0)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetAddFailedMessage("Results"));
            }
        }

         public async Task<IEnumerable<ResultReadForStudentDto>> GetAllResultForSpecificStudent(String code,string semester)
         {
            var results = await _unitOfWork.ResultRepo.GetAllAsync(r=>r.Semester==semester && r.StudentId==code);
            if (results.Any(r=>r.Grade==-1))
            {
                var resultsDto = results.Select(r => new ResultReadForStudentDto
                {
                    StudentId = r.StudentId,
                    CourseName = r.Course.Name,
                    StudentName = r.Student.Name,
                }).ToList();
                return resultsDto;
            }
            else
            {
                var resultsDto = results.Select(r => new ResultReadForStudentDto
                {
                    StudentId = r.StudentId,
                    CourseName = r.Course.Name,
                    StudentName = r.Student.Name,
                    Grade = _helper.GetGradeWithSymbol(r.Grade),
                }).ToList();
                return resultsDto;
            }      
         }
        public async Task<IEnumerable<ResultReadForAdminDto>> GetAllResultForAdmin(String code, string semester)
        {
            var results = await _unitOfWork.ResultRepo.GetAllAsync(r => r.Semester == semester && r.StudentId == code);
            if(!results.Any() || results==null)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetNotFoundMessage("Result"));
            }
                var resultsDto = results.Select(r => new ResultReadForAdminDto
                {
                   CourseId=r.CourseCode,
                   CourseName=r.Course.Name,
                   StudentName=r.Student.Name,
                   StudentId=r.StudentId,
                   Grade=r.Grade,
                   GradeWithSymbol=_helper.GetGradeWithSymbol(r.Grade),
                }).ToList();
                return resultsDto;
        }

        public async Task<IEnumerable<ResultsReadForAllStudents>> GetAllResultsForAllStudents(string semester)
        {
            var allResults = await _unitOfWork.ResultRepo.GetAllAsync(r => r.Semester == semester);
            if(!allResults.Any() || allResults==null)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetNotFoundMessage("Results"));
            }
            var allResultsDto = allResults.GroupBy(r => r.StudentId)
                .Select(g => new ResultsReadForAllStudents
                {
                    Results=g.Select(i=>new ResultReadForAdminDto
                    {
                        StudentId=i.StudentId,
                        Grade=i.Grade,
                        CourseId=i.CourseCode,
                        CourseName=i.Course.Name,
                        StudentName=i.Student.Name,
                        GradeWithSymbol=_helper.GetGradeWithSymbol(i.Grade),
                    }).OrderBy(o=>o.StudentName).ToList(),
                    TotalCount=g.Count()
                }).ToList();
            return allResultsDto;
        }
        [Obsolete]
        public async Task AddResultWithExcel(Stream results)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package= new ExcelPackage(results))
            {
                var workSheet = package.Workbook.Worksheets[0];
                int rowCount= workSheet.Dimension.Rows;
                var resultsList = new List<StudentCourse>();
                for (int i=2; i<= rowCount; i++)
                {
                    var studentId = workSheet.Cells[i, 1]?.Value?.ToString()?.Trim();
                    var courseId = workSheet.Cells[i, 2]?.Value?.ToString()?.Trim();
                    var gradeOfFinal = int.TryParse(workSheet.Cells[i, 3]?.Value?.ToString(), out var final) ? final : 0;
                    var gradeWithoutFinal = int.TryParse(workSheet.Cells[i, 4]?.Value?.ToString(), out var nonFinal) ? nonFinal : 0;
                    var semester = workSheet.Cells[i, 5]?.Value?.ToString()?.Trim();

                    if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(courseId))
                        continue;
                    var result = new StudentCourse()
                    {
                        StudentId = studentId,
                        CourseCode= courseId,
                        Grade=gradeOfFinal+gradeWithoutFinal,
                        Semester=semester,
                        IsPassed=(gradeOfFinal>=15 && (gradeOfFinal+gradeWithoutFinal>=50))
                        
                    };
                  await _unitOfWork.ResultRepo.Update(result);
                    int isAdded = await _unitOfWork.Complete();
                    if (isAdded == 0)
                    {
                        throw new Exception(Exceptions.ExceptionMessages.GetAddFailedMessage("Results"));
                    }
                    var student = await _unitOfWork.StudentRepo.GetByIdAsync(studentId);
                    var PassedCourses = await _unitOfWork.ResultRepo.GetAllAsync(r => r.IsPassed && r.StudentId == studentId);
                    var Hours = PassedCourses.Select(c => c.Course.Hours).Sum();
                    int currentSemester = ConstantData.SemestersByName[student.Semester];
                    if (currentSemester % 2 == 0)
                    {
                        currentSemester = _helper.GoToNextSemester(Hours, currentSemester);
                    }
                    else
                    {
                        currentSemester++;
                    }
                    var gpa = _helper.CalculateGPA(PassedCourses);
                    student.Semester = ConstantData.SemestersByNumber[currentSemester];
                    student.Gpa = gpa;
                    await _unitOfWork.StudentRepo.Update(student);
                    int isUpdated = await _unitOfWork.Complete();
                    if (isAdded == 0)
                    {
                        throw new Exception(Exceptions.ExceptionMessages.GetAddFailedMessage("Results"));
                    }
                }
            }

        }
    }
}
