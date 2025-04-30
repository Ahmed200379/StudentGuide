using StudentGuide.API.Helpers;
using StudentGuide.BLL.Constant;
using StudentGuide.BLL.Dtos.Student;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;


namespace StudentGuide.BLL.Services.Students
{
  public class StudentService:IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelper _helper;
        public StudentService(IUnitOfWork unitOfWork,IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }
        public async Task<int> GetMaxHours(string code)
        {
            var student = await _unitOfWork.StudentRepo.GetByIdAsync(code);
            if (student == null)
            {
                throw new Exception("No Student Found");
            }
            if (student.Semester == "Summer")
            {
                return 6;
            }
            else if (student.Gpa >= 2 || student.Gpa == 0)
            {
                return 18;
            }
            else if (student.Gpa > 1 && student.Gpa < 2)
            {
                return 15;
            }
            else
            {
                return 12;
            }
        }

        public async Task AddNewStudent(StudentAddDto newStudent)
        {
            if (newStudent == null)
            {
                throw new Exception("All fields required");
            }
            Student? anyStudent= await _unitOfWork.StudentRepo.GetByIdAsync(newStudent.StudentId);

            if (anyStudent != null)
            {
                throw new Exception("This Student Added before");
            }
            var addStudent = new Student
            {
                Name=newStudent.StudentName,
                Code = newStudent.StudentId,
                Email = newStudent.StudentEmail,
                Password = newStudent.StudentPassword,
                Gpa = newStudent.StudentGpa,
                Hours = newStudent.TotalHours,
                Date = newStudent.DateOfRegister,
                Photo = newStudent.StudentPhoto,
                BirthDate = newStudent.BirthDateOfStudent,
                PhoneNumber = newStudent.PhoneNumber,
                Semester = newStudent.Semester,
                DepartmentCode = newStudent.DepartmentCode,
            };

            await _unitOfWork.StudentRepo.AddAsync(addStudent);
            int isSaved = await _unitOfWork.Complete();
            if (isSaved == 0)
            {
                throw new Exception("Failed to add Student");
            }
        }

        public async Task Delete(string code)
        {
            var student=await _unitOfWork.StudentRepo.GetByIdAsync(code);
            if(student == null)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetNotFoundMessage("Student"));
            }
           await _unitOfWork.StudentRepo.Delete(student);
            int isDeleted = await _unitOfWork.Complete();
            if (isDeleted == 0)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetDeleteFailedMessage("Student"));
            }
        }

        public async Task EditStudent(StudentEditDto editStudent)
        {
            var student = await _unitOfWork.StudentRepo.GetByIdAsync(editStudent.StudentId);
            if(student==null)
            {
                throw new Exception("Student does not found");
            }
            _helper.MapStudentEditDtoToStudent(editStudent, student);
           await _unitOfWork.StudentRepo.Update(student);
            int isUpdated = await _unitOfWork.Complete();
            if(isUpdated ==0)
            {
                throw new Exception("Faild to update");
            }

        }

        public async Task EnrollCourses(StudentErollDto studentErollDto)
        {
            var student = await _unitOfWork.StudentRepo.GetByIdAsync(studentErollDto.StudentId);
            if(student==null)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetNotFoundMessage("Student"));
            }
            else if(_helper.HasDuplicates(studentErollDto.Codes))
            {
                throw new Exception("There are duplicate courses in your selection.");
            }
            var allCourse = await _unitOfWork.CourseRepo.GetAllAsync(c => studentErollDto.Codes.Contains(c.Code));
            var totalHours = allCourse.Sum(c => c.Hours);
            var maxHoursForStudent = await GetMaxHours(studentErollDto.StudentId);
            if (totalHours> maxHoursForStudent)
            {
                throw new Exception("you are limited max Hours");
            }
            var studentCourses = allCourse.Select(
                c => new StudentCourse
                {
                    CourseCode=c.Code,
                    Grade=-1,
                    StudentId=student.Code,
                    IsPassed=false
                }).ToList();
            await _unitOfWork.StudentRepo.AddRangeAsync(studentCourses);
            int isAdded = await _unitOfWork.Complete();
            if(isAdded==0)
            {
                throw new Exception("Failed to enrollment Courses");
            }
        }

        public async Task<StudentReadWithCountDto> GetAllStudentsInPagnation(int page, int countPerPage)
        {
            var allStudents=await _unitOfWork.StudentRepo.GetAllStudentsInPagnation(page, countPerPage);
            var TotalStudents = await _unitOfWork.StudentRepo.TotalCount();
            var allStudentsDto = allStudents.Select(
                s => new StudentReadForAdminDto
                {
                    StudentId = s.Code,
                    StudentName = s.Name,
                    StudentEmail = s.Email,
                    DepartmentName = s.Department.Name,
                    PhoneNumber = s.PhoneNumber,
                    Semester = s.Semester,
                    BirthDateOfStudent = s.BirthDate,
                    StudentGpa = s.Gpa,
                    StudentPassword = s.Password,
                    StudentPhoto = s.Photo,
                    TotalHours = s.Hours,
                    DateOfRegister = s.Date
                });
            var allStudentsWithCount = new StudentReadWithCountDto()
            {
                StudentReadForAdmins = allStudentsDto,
                TotalCount = TotalStudents
            };
            return allStudentsWithCount;
        }

        public async Task<StudentReadWithCountDto> GetAllSudentsWithCount()
        {
            var allStudents= await _unitOfWork.StudentRepo.GetAllAsync();
            var allStudentDto = allStudents
                .Select(p => new StudentReadForAdminDto
                {
                    StudentId=p.Code,
                    StudentName=p.Name,
                    StudentEmail=p.Email,
                    DepartmentName= p.Department.Name,
                    PhoneNumber=p.PhoneNumber,
                    Semester=p.Semester,
                    BirthDateOfStudent=p.BirthDate,
                    StudentGpa=p.Gpa,
                    StudentPassword=p.Password,
                    StudentPhoto=p.Photo,
                    TotalHours=p.Hours,
                    DateOfRegister=p.Date
                }).ToList();
            var allStudentsWithCountDto = new StudentReadWithCountDto()
            {
                StudentReadForAdmins = allStudentDto,
                TotalCount = allStudents.Count()
            };
            return allStudentsWithCountDto;
        }

        public async Task<StudentReadForAdminDto> GetByIdForAdmin(string code)
        {
            var student= await _unitOfWork.StudentRepo.GetByIdAsync(code);
            if(student == null)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetNotFoundMessage("Student"));
            }
            var studentDto = new StudentReadForAdminDto
            {
                StudentName = student.Name,
                StudentEmail = student.Email,
                DepartmentName = student.Department.Name,
                PhoneNumber = student.PhoneNumber,
                Semester = student.Semester,
                BirthDateOfStudent = student.BirthDate,
                StudentGpa = student.Gpa,
                StudentPassword = student.Password,
                StudentPhoto = student.Photo,
                TotalHours = student.Hours,
                DateOfRegister = student.Date
            };
            return studentDto;
        }

        public async Task<StudentReadForStudentDto> GetByIdForStudent(string code)
        {
            var student = await _unitOfWork.StudentRepo.GetByIdAsync(code);
            if (student == null)
            {
                throw new Exception(Exceptions.ExceptionMessages.GetNotFoundMessage("Student"));
            }
            var studentDto = new StudentReadForStudentDto
            {
              DepartmentName= student.Department.Name,
              StudentEmail = student.Email,
              PhoneNumber= student.PhoneNumber,
              Semester = student.Semester,
              StudentGpa= student.Gpa,
              StudentName= student.Name,
              StudentPhoto= student.Photo,
              TotalHours= student.Hours,
            };
            return studentDto;
        }
        

      

    }
}
