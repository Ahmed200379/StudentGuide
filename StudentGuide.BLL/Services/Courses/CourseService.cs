using FuzzySharp;
using StudentGuide.BLL.Dtos.Course;
using StudentGuide.BLL.Dtos.Material;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task DeleteCourse(int id)
        {
            var deletedCourse = await _unitOfWork.CourseRepo.GetByIdAsync(id);
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

            var materialDto = courses.Select(p => new CourseReadDto
            {
               Code= p.Code,
               NameOfCourse= p.Name,
               HoursOfCourse= p.Hours,
               MandatoryCourse= p.IsCompulsory,
               PreRequestCoursesCode= p.PrerequisiteCourses!.ToList(),
               Semesters = p.Semesters.ToList(),
               DepartmentIds=p.CourseDepartments.Select(p=> p.DepartmentsCode).ToList(),
            }).ToList();
            if (string.IsNullOrWhiteSpace(Keyword)) return materialDto;

            Keyword = Keyword.Trim().ToLower();

            // Step 2: Fuzzy filter in memory
            var results = materialDto
                .Select(m => new
                {
                    Material = m,
                    Score = Fuzz.TokenSetRatio(Keyword, $"{m.NameOfCourse}{m.Code}")
                })
                .Where(x => x.Score > 30) // You can adjust this threshold
                .OrderByDescending(x => x.Score)
                .Select(x => x.Material)
                .ToList();

            return results;
        }

    }
}
