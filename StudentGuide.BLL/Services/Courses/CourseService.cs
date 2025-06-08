using FuzzySharp;
using StudentGuide.BLL.Dtos.Course;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using StudentGuide.BLL.Constant;
using StudentGuide.BLL.Services.Students;
using StudentGuide.API.Helpers;
using System.Linq;
namespace StudentGuide.BLL.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentService _studentService;
        private readonly IHelper _helper;
        public CourseService(IUnitOfWork unitOfWork, IStudentService studentService,IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _studentService= studentService;
            _helper = helper;
        }
        public async Task AddCourse(CourseAddDto newCourse)
        {
            if (newCourse == null)
            {
                throw new Exception("All fields required");
            }
            Course? anyCourses = await _unitOfWork.CourseRepo.GetByIdAsync(newCourse.NameOfCourse);
            if(anyCourses !=null)
            {
                throw new Exception("This Course Added before");
            }
            Course addCourse = new Course
            {
                Code = newCourse.Code,
                Name = newCourse.NameOfCourse,
                CourseDepartments = newCourse.DepartmentIds.Select(d => new CourseDepartment { DepartmentsCode = d, CoursesCode=newCourse.Code }).ToList(),
                Hours = newCourse.HoursOfCourse,
                IsCompulsory = newCourse.MandatoryCourse,
                PrerequisiteCourses = newCourse.PreRequestCoursesCode.ToList(),
                Semesters = newCourse.Semesters.ToList()
            };
            await _unitOfWork.CourseRepo.AddAsync(addCourse);
            int isSaved = await _unitOfWork.Complete();
            if(isSaved == 0)
            {
                throw new Exception("Failed to add course");
            }
        }

        public async Task DeleteCourse(string code)
        {
            var deletedCourse = await _unitOfWork.CourseRepo.GetByIdAsync(code);
            if (deletedCourse == null)
            {
                throw new Exception("Course not exist");
            }
           await _unitOfWork.CourseRepo.Delete(deletedCourse);
            int isDeleted = await _unitOfWork.Complete();
            if (isDeleted == 0)
            {
                throw new Exception("Failed to delete course");
            }
        }

        public async Task EditCourse(CourseEditDto course)
        {
            Course? editCourse = await _unitOfWork.CourseRepo.GetByIdAsync(course.Code);
            if (editCourse == null)
            {
                throw new Exception("Course not exist");
            }
            editCourse.Name = course.NameOfCourse;
            editCourse.Hours = course.HoursOfCourse;
            editCourse.IsCompulsory = course.MandatoryCourse;
            editCourse.PrerequisiteCourses = course.PreRequestCoursesCode.ToList();
            editCourse.Semesters = course.Semesters.ToList();
            editCourse.CourseDepartments = course.DepartmentIds.Select(d => new CourseDepartment { DepartmentsCode = d, CoursesCode = course.Code }).ToList();
            await _unitOfWork.CourseRepo.Update(editCourse);
            int isUpdated = await _unitOfWork.Complete();
            if (isUpdated == 0)
            {
                throw new Exception("Failed to update course");
            }
        }

        public async Task<IEnumerable<CourseReadDto>> GetAllCourses()
        {
            var allCourses =  await _unitOfWork.CourseRepo.GetAllAsync();
            if (allCourses == null)
            {
                throw new Exception("No courses found");
            }
            var allCoursesDto = allCourses.Select(c => new CourseReadDto
            {
                Code = c.Code,
                NameOfCourse = c.Name,
                HoursOfCourse = c.Hours,
                MandatoryCourse = c.IsCompulsory,
                PreRequestCoursesCode = c.PrerequisiteCourses!.ToList(),
                Semesters = c.Semesters.ToList(),
                DepartmentIds = c.CourseDepartments.Select(cd => cd.DepartmentsCode).ToList()
            }).ToList();
            return allCoursesDto;
        }
          public async Task<CourseReadPagnationDto> GetAllCoursesInPagnation(int page, int countPerPage)
          {
              var allCourses = await _unitOfWork.CourseRepo.GetAllCoursesInPagnation(page, countPerPage);
              if (allCourses == null)
              {
                  throw new Exception("No courses found");
              }
              var totalCount = await _unitOfWork.CourseRepo.TotalCount();
            var allCoursesDto = allCourses.Select(c => new CourseReadDto
            {
                Code = c.Code,
                NameOfCourse = c.Name,
                HoursOfCourse = c.Hours,
                MandatoryCourse = c.IsCompulsory,
                PreRequestCoursesCode = c.PrerequisiteCourses!.ToList(),
                Semesters = c.Semesters.ToList(),
                DepartmentIds = c.CourseDepartments.Select(cd => cd.DepartmentsCode).ToList()
            }).ToList();
            var courseReadPagnationDto = new CourseReadPagnationDto
            {
                Courses = allCoursesDto,
                TotalCount = totalCount
            };
            return courseReadPagnationDto;
        }

        public async Task<CourseReadDto?> GetCourseById(String code)
        {
            Course? course = await _unitOfWork.CourseRepo.GetByIdAsync(code);
            if (course == null)
                throw new Exception("No course Found");
            var courseDto = new CourseReadDto{
                Code= course.Code,
                NameOfCourse= course.Name,
                HoursOfCourse= course.Hours,
                MandatoryCourse= course.IsCompulsory,
                PreRequestCoursesCode= course.PrerequisiteCourses!.ToList(),
                DepartmentIds=course.CourseDepartments.Select(p=>p.DepartmentsCode).ToList(),
                Semesters= course.Semesters.ToList(),
            };
            return courseDto;
        }
        public async Task<List<CourseReadDto>> Search(string Keyword)
        {
            var courses = await _unitOfWork.CourseRepo.GetAll();

            var courseDto = courses.Select(p => new CourseReadDto
            {
               Code= p.Code,
               NameOfCourse= p.Name,
               HoursOfCourse= p.Hours,
               MandatoryCourse= p.IsCompulsory,
               PreRequestCoursesCode= p.PrerequisiteCourses!.ToList(),
               Semesters = p.Semesters.ToList(),
               DepartmentIds=p.CourseDepartments.Select(p=> p.DepartmentsCode).ToList(),
            }).ToList();
            if (string.IsNullOrWhiteSpace(Keyword)) return courseDto;

            Keyword = Keyword.Trim().ToLower();

            // Step 2: Fuzzy filter in memory
            var results = courseDto
                .Select(c => new
                {
                    Course = c,
                    Score = Fuzz.TokenSetRatio(Keyword, $"{c.NameOfCourse}{c.Code}")
                })
                .Where(x => x.Score > 30) // You can adjust this threshold
                .OrderByDescending(x => x.Score)
                .Select(x => x.Course)
                .ToList();
            if (!results.Any()) return courseDto;
            return results;
        }


        public async Task<CourseReadForStudentDto> GetAllCoursesForStudent(String code)
        {
            var student = await _unitOfWork.StudentRepo.GetByIdAsync(code);
            if (student == null)
            {
                throw new Exception("No Student Found");
            }
            var departmentCodeForStudent = student.DepartmentCode;
            var maxHours = await _studentService.GetMaxHours(code);
            var checkSemester = ConstantData.SemestersByName[student.Semester];

            IEnumerable<Course> courses = new List<Course>();

            // Get courses based on the student's semester (even or odd)
            var allCourses = await _unitOfWork.CourseRepo.GetAllAsync();
            // Handling other semesters
            var passedCourses = courses
                .Where(c => c.Students.Any(s => s.StudentId == code && s.IsPassed))
                .ToList();
            if (checkSemester % 2 == 0 && checkSemester != 0)
            {
                courses = allCourses.Where(c =>
                    c.Semesters.Any(s => ConstantData.SemestersByName.ContainsKey(s) && ConstantData.SemestersByName[s] % 2 == 0));
            }
            else if (checkSemester % 2 != 0)
            {
                courses = allCourses.Where(c =>
                    c.Semesters.Any(s => ConstantData.SemestersByName.ContainsKey(s) && ConstantData.SemestersByName[s] % 2 != 0));
            }
            else
            {
                courses = allCourses.Where(c => c.Semesters.Contains(student.Semester));
            }


            // Handling semester 1 (Special Case)
            if (checkSemester == 1)
            {
                var availableCourses = courses
                    .Where(c => c.Semesters.Contains(student.Semester))
                    .Select(c=>_helper.MapToCourseReadDto(c))
                    .ToList();

                return new CourseReadForStudentDto
                {
                    AllAvaliableCourses = availableCourses,
                    HoursOfStudent = student.Hours,
                    MaxHours = maxHours
                };
            }

            

            var passedCoursesCode = passedCourses.Select(c => c.Code).ToList();

            var totalHoursForHumanCourses = passedCourses
                .Where(c => c.CourseDepartments.Any(d => d.DepartmentsCode == "HM"))
                .Sum(c => c.Hours);

            var totalHoursForElectiveCourses = passedCourses
                .Where(c => !c.IsCompulsory)
                .Sum(c => c.Hours);

            // Apply additional filters
            if (totalHoursForElectiveCourses >= 63)
            {
                courses = courses.Where(c => c.IsCompulsory).ToList();
            }

            if (totalHoursForHumanCourses >= 12)
            {
                courses = courses.Where(c => !c.CourseDepartments.Any(d => d.DepartmentsCode == "HM")).ToList();
            }

            var availableCoursesFiltered = courses
                .Where(c => !c.Students.Any(s => s.StudentId == code && s.IsPassed)
                    && (c.PrerequisiteCourses.Count == 0 || c.PrerequisiteCourses.All(p => passedCoursesCode.Contains(p)))
                    && c.CourseDepartments.Any(d => d.DepartmentsCode == departmentCodeForStudent || d.DepartmentsCode=="General")
                    )
                .Select(c => _helper.MapToCourseReadDto(c))
                .ToList();

            return new CourseReadForStudentDto
            {
                AllAvaliableCourses = availableCoursesFiltered,
                HoursOfStudent = student.Hours,
                MaxHours = maxHours
            };
        }

        public async Task<IEnumerable<CourseReadDto>> GetRecommendationCourses(string code)
        {
            var student = await _unitOfWork.StudentRepo.GetByIdAsync(code);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            var studentSemester = ConstantData.SemestersByName[student.Semester];
            var allCourses = await GetAllCoursesForStudent(code);
            if (allCourses == null || !allCourses.AllAvaliableCourses.Any())
            {
                return new List<CourseReadDto>();
            }

            var prevObligatory = new List<CourseReadDto>();
            var currentObligatory = new List<CourseReadDto>();
            var prevOptional = new List<CourseReadDto>();
            var currentOptional = new List<CourseReadDto>();
            var other = new List<CourseReadDto>();

            foreach (var course in allCourses.AllAvaliableCourses)
            {
                var semesterNumbers = course.Semesters
                    .Where(s => ConstantData.SemestersByName.ContainsKey(s))
                    .Select(s => ConstantData.SemestersByName[s])
                    .ToList();

                if (semesterNumbers.Contains(studentSemester))
                {
                    if (course.MandatoryCourse)
                        currentObligatory.Add(course);
                    else
                        currentOptional.Add(course);
                }
                else if (semesterNumbers.Any(s => s < studentSemester))
                {
                    if (course.MandatoryCourse)
                        prevObligatory.Add(course);
                    else
                        prevOptional.Add(course);
                }
                else
                {
                    other.Add(course);
                }
            }

            // Final recommendation order: prevObligatory → currentObligatory → prevOptional → currentOptional → other
            return prevObligatory
                .Concat(currentObligatory)
                .Concat(prevOptional)
                .Concat(currentOptional)
                .Concat(other)
                .ToList();
        }

    }
}
