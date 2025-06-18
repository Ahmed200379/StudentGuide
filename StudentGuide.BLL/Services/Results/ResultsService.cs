
using NPOI.XSSF.UserModel;
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
            var isFinished = await _unitOfWork.ResultRepo.GetAllAsync(c => c.Grade == -1 && c.StudentId == student.Code);
            if (!isFinished.Any())
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
                if(!isAddedSecondTime)
                {
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
                    Grade = r.Grade == -1 ? "Not Graded Yet" : _helper.GetGradeWithSymbol(r.Grade),
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
            try
            {
                using var workbook = new XSSFWorkbook(results);
                var sheet = workbook.GetSheetAt(0);

                if (sheet == null || sheet.LastRowNum < 1)
                    throw new Exception("Excel file is empty or has no data rows");

                var resultDtos = new List<ResultAddDto>();

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null) continue;

                    var studentId = row.GetCell(0)?.ToString()?.Trim();
                    var courseId = row.GetCell(1)?.ToString()?.Trim();

                    if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(courseId))
                        continue;

                    int.TryParse(row.GetCell(2)?.ToString(), out var gradeOfFinal);
                    int.TryParse(row.GetCell(3)?.ToString(), out var gradeWithoutFinal);

                    resultDtos.Add(new ResultAddDto
                    {
                        StudentId = studentId,
                        CourseId = courseId,
                        GradeOfFinal = gradeOfFinal,
                        GradeWithoutFinal = gradeWithoutFinal
                    });
                }

                if (!resultDtos.Any())
                    throw new Exception("No valid data found in Excel file");

                // Process in batches by student to optimize DB operations
                var groupedByStudent = resultDtos.GroupBy(r => r.StudentId);
                foreach (var studentGroup in groupedByStudent)
                {
                    await ProcessStudentResults(studentGroup.ToList());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to process Excel file: " + ex.Message, ex);
            }
        }

        private async Task ProcessStudentResults(List<ResultAddDto> results)
        {
            if (results == null || !results.Any())
                return;

            var studentId = results.First().StudentId;
            var student = await _unitOfWork.StudentRepo.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception($"Student not found: {studentId}");

            // Get all existing results for this student
            var existingResults = await _unitOfWork.ResultRepo.GetAllAsync(r => r.StudentId == studentId);

            var resultsToUpdate = new List<StudentCourse>();
            var passedCourses = new List<StudentCourse>();
            var totalHoursGained = 0;

            foreach (var resultDto in results)
            {
                var totalGrade = resultDto.GradeOfFinal + resultDto.GradeWithoutFinal;
                var isPassed = resultDto.GradeOfFinal >= 15 && totalGrade >= 50;

                var existingResult = existingResults.FirstOrDefault(r => r.CourseCode == resultDto.CourseId);

                if (existingResult == null)
                {
                    // New result
                    var newResult = new StudentCourse
                    {
                        CourseCode = resultDto.CourseId,
                        StudentId = resultDto.StudentId,
                        Grade = totalGrade,
                        IsPassed = isPassed,
                        Semester = student.Semester
                    };
                    resultsToUpdate.Add(newResult);

                    if (isPassed)
                    {
                        passedCourses.Add(newResult);
                        totalHoursGained += await _unitOfWork.ResultRepo.GetHoursOfCourse(resultDto.CourseId);
                    }
                }
                else if (!existingResult.IsPassed && isPassed)
                {
                    // Update previously failed result
                    existingResult.Grade = totalGrade;
                    existingResult.IsPassed = true;
                    resultsToUpdate.Add(existingResult);
                    passedCourses.Add(existingResult);
                    totalHoursGained += await _unitOfWork.ResultRepo.GetHoursOfCourse(resultDto.CourseId);
                }
            }

            if (resultsToUpdate.Any())
            {
                _unitOfWork.ResultRepo.UpdateRangeAsync(resultsToUpdate);
                var saveResult = await _unitOfWork.Complete();
                if (saveResult == 0)
                    throw new Exception("Failed to save results to database");
            }

            if (passedCourses.Any())
            {
                // Update student GPA and hours
                var allPassedCourses = existingResults.Where(r => r.IsPassed).Concat(passedCourses);
                student.Gpa = await _helper.CalculateGPA(allPassedCourses);
                student.Hours += totalHoursGained;

                // Check if student has completed current semester
                var unfinishedCourses = await _unitOfWork.ResultRepo.GetAllAsync(
                    c => c.Grade == -1 && c.StudentId == student.Code);

                if (!unfinishedCourses.Any())
                {
                    var currentSemesterNumber = ConstantData.SemestersByName[student.Semester];

                    int nextSemesterNumber = currentSemesterNumber % 2 == 0
                        ? _helper.GoToNextSemester(student.Hours, currentSemesterNumber)
                        : currentSemesterNumber + 1;

                    // Convert number back to semester name
                    student.Semester = ConstantData.SemestersByNumber[nextSemesterNumber];
                }

                await _unitOfWork.StudentRepo.Update(student);
                var updateResult = await _unitOfWork.Complete();
                if (updateResult == 0)
                    throw new Exception("Failed to update student record");
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
