using ClosedXML.Excel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using StudentGuide.API.Helpers;
using StudentGuide.BLL.Constant;
using StudentGuide.BLL.Dtos.Account;
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
            var result =await _unitOfWork.ResultRepo.GetAllAsync(r=>r.CourseCode==results.First().CourseId);
            var isAddedSecondTime = result.First().IsPassed;
            var student = await _unitOfWork.StudentRepo.GetByIdAsync(results.First().StudentId);
            if (student == null)
                throw new Exception(Exceptions.ExceptionMessages.GetNotFoundMessage("Student"));
            var resultsOfStudent = results.Select(r=>
                new StudentCourse
                {
                    CourseCode=r.CourseId,
                    StudentId=r.StudentId,
                    Grade=r.GradeOfFinal+r.GradeWithoutFinal,
                    IsPassed=(r.GradeOfFinal>=15 && (r.GradeOfFinal+r.GradeWithoutFinal)>=50),
                    Semester=student.Semester,
                }).ToList();
             _unitOfWork.ResultRepo.UpdateRangeAsync(resultsOfStudent);
            int isAdded = await _unitOfWork.Complete();
            if (isAdded == 0)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetAddFailedMessage("Results"));
            }
            var Hours = student.Hours;
            int currentSemester = ConstantData.SemestersByName[student.Semester];
            if(await _unitOfWork.ResultRepo.GetAllAsync(c=>c.Grade== -1 && c.StudentId == student.Code) is null)
            {
                if (currentSemester % 2 == 0)
                {
                    currentSemester = _helper.GoToNextSemester(Hours, currentSemester);
                }
                else
                {
                    currentSemester++;
                }
            }
            var isPassed = (results.First().GradeOfFinal >= 15 && (results.First().GradeOfFinal + results.First().GradeWithoutFinal) >= 50);
            if (isPassed)
            {
                var codeOfPassedCourse = results.FirstOrDefault()!.CourseId;
                var HoursOfPassedCourse = await _unitOfWork.ResultRepo.GetHoursOfCourse(codeOfPassedCourse);
                var allPassedCourses = await _unitOfWork.ResultRepo.GetAllAsync(c => c.StudentId == student.Code && c.IsPassed);
                var gpa = await  _helper.CalculateGPA(allPassedCourses);
                student.Semester = ConstantData.SemestersByNumber[currentSemester];
                student.Gpa = gpa;
                if(isAddedSecondTime!)                {
                    student.Hours += HoursOfPassedCourse;
                }
                await _unitOfWork.StudentRepo.Update(student);
                int isUpdated = await _unitOfWork.Complete();
                if (isAdded == 0)
                {
                    throw new Exception(Exceptions.ExceptionMessages.GetAddFailedMessage("Results"));
                }
            }
           
        }
         public async Task<IEnumerable<ResultReadForStudentDto>> GetAllResultForSpecificStudent(ResultReadForResult specificUser)
         {
            var results = await _unitOfWork.ResultRepo.GetAllWithIncludeAsync(
                   r => r.Semester == specificUser.Semester && r.StudentId == specificUser.StudentId);
            
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
        public async Task<IEnumerable<ResultReadForAdminDto>> GetAllResultForAdmin(ResultReadForResult specificUser)
        {
            var results = await _unitOfWork.ResultRepo.GetAllWithIncludeAsync(
                   r => r.Semester == specificUser.Semester && r.StudentId == specificUser.StudentId);
            if (!results.Any() || results==null)
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
            var allResults = await _unitOfWork.ResultRepo.GetAllWithIncludeAsync(r=>r.Semester==semester);
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
            var workbook = new XSSFWorkbook(results);
            var sheet = workbook.GetSheetAt(0);
            int rowCount = sheet.LastRowNum;

            for (int i = 1; i <= rowCount; i++) // Assuming header is at row 0
            {
                var row = sheet.GetRow(i);
                if (row == null) continue;

                var studentId = row.GetCell(0)?.ToString()?.Trim();
                var courseId = row.GetCell(1)?.ToString()?.Trim();

                var finalGradeCell = row.GetCell(2);
                var nonFinalGradeCell = row.GetCell(3);
                var semester = row.GetCell(4)?.ToString()?.Trim();

                if (string.IsNullOrEmpty(studentId) ||
                    string.IsNullOrEmpty(courseId))
                    continue;

                int gradeOfFinal = (finalGradeCell != null &&
                                    int.TryParse(finalGradeCell.ToString(), out var final))
                                        ? final : 0;

                int gradeWithoutFinal = (nonFinalGradeCell != null &&
                                           int.TryParse(nonFinalGradeCell.ToString(), out var nonFinal))
                                              ? nonFinal : 0;

                var result = new ResultAddDto
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    GradeOfFinal = gradeOfFinal,
                    GradeWithoutFinal = gradeWithoutFinal,
                };

                // Apply the previously implemented per-result logic
                await AddResult(new List<ResultAddDto> { result });
            }
        }

        public async Task<MessageResponseDto> DeleteCourse(ResultDeleteDto reseltDeleteDto)
        {
            var Course = _unitOfWork.ResultRepo.GetById(reseltDeleteDto.studentId, reseltDeleteDto.courseCode);
            if (Course == null)
            {
                return new MessageResponseDto
                {
                    Message = "Course not found",
                };
            }
            if (Course.IsPassed ==true)
            {
               return new MessageResponseDto
               {
                   Message = "You can't delete a course that has a grade",
               };
            }
            await _unitOfWork.ResultRepo.Delete(Course);
            int isDeleted = await _unitOfWork.Complete();
            if (isDeleted == 0)
            {
                return new MessageResponseDto
                {
                    Message = "Failed to delete course",
                };
            }
            return new MessageResponseDto
            {
                Message = "Course deleted successfully",
            };
        }
    }
}
