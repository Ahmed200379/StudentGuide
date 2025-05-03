using StudentGuide.BLL.Dtos.Material;
using StudentGuide.DAL.Repos.MaterialRepo;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.HttpResults;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;

namespace StudentGuide.BLL.Services.Materials
{
   public class MaterialService : IMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MaterialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetAllMaterial
        public async Task<IEnumerable<MaterialReadDto>> GetAllMaterial()
        {
            IEnumerable<Material> AllMaterialFromDb = await _unitOfWork.MaterialRepo.GetAll();
            IEnumerable<MaterialReadDto> AllMaterial = AllMaterialFromDb
                .Select(p => new MaterialReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Instructor=p.InstructorName,
                    DriveLink = p.Drive,
                    YoutubeLink = p.Youtube,
                    CourseCode=p.CourseCode
                });
            return AllMaterial;
        }
# region GetMaterialBYId
        public async Task<MaterialReadDto?> GetMaterialById(int id)
        {
            Material? MaterialFromDb = await _unitOfWork.MaterialRepo.GetByIdAsync(id);
            if(MaterialFromDb is null) { return null; }
            MaterialReadDto material = new()
            {
                Id = MaterialFromDb.Id,
                Name = MaterialFromDb.Name,
                Instructor = MaterialFromDb.InstructorName,
                DriveLink = MaterialFromDb.Drive,
                YoutubeLink = MaterialFromDb.Youtube,
                CourseCode=MaterialFromDb.CourseCode
            };
            return material;
        }
        #endregion
        public async Task<MaterialReadDto?> GetMaterialBYname(string Name)
        {
            Material MaterialFromDb = await _unitOfWork.MaterialRepo.GetMaterialByNameAsync(Name);
            if (MaterialFromDb is null) return null;
            MaterialReadDto material = new()
            {
                DriveLink = MaterialFromDb.Drive,
                Instructor = MaterialFromDb.InstructorName,
                Name = MaterialFromDb.Name,
                YoutubeLink = MaterialFromDb.Youtube,
                CourseCode = MaterialFromDb.CourseCode
            };
            return material;
        }
        #endregion
        #region AddMaterial
        public async Task<bool> AddNewMaterial(MaterialAddDto NewMaterial)
        {
            try
            {
                Material material = new()
                {
                    Name = NewMaterial.Name,
                    Drive = NewMaterial.DriveLink,
                    Youtube = NewMaterial.YoutubeLink,
                    InstructorName = NewMaterial.Instructor,
                    CourseCode = NewMaterial.CourseCode
                };
                await _unitOfWork.MaterialRepo.AddAsync(material);
                int issaved = await _unitOfWork.Complete();
                return issaved > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
          
        }
        #endregion
        public async Task<bool> DeleteMaterial(int id)
        {
            Material? deletedMaterial = await _unitOfWork.MaterialRepo.GetByIdAsync(id);
            if(deletedMaterial is null)
            {
                return false;
            }
            else
            {
               await _unitOfWork.MaterialRepo.Delete(deletedMaterial);
                int issaved = await _unitOfWork.Complete();
                return issaved > 0;
            }
        }

        public async Task<bool> EditMaterial(MaterialEditDto materialEditDto)
        {
            Material? material = await _unitOfWork.MaterialRepo.GetByIdAsync(materialEditDto.Id);
            if(material == null)
            {
                return false;
            }
            else
            {
                material.Name = materialEditDto.Name;
                material.InstructorName = materialEditDto.Instructor;
                material.Youtube = materialEditDto.YoutubeLink;
                material.Drive = materialEditDto.DriveLink;
                material.CourseCode = materialEditDto.CourseCode;

               await _unitOfWork.MaterialRepo.Update(material);
                int isupdated = await _unitOfWork.Complete();
                return isupdated > 0;
            }
        }

        public async Task<MaterialReadPagnationDto> GetAllMaterialInPagnation(int page, int countPerPage)
        {
            var materials = await _unitOfWork.MaterialRepo.GetAllMaterialsInPagnation(page, countPerPage);
            var materialDto=materials.Select(p => new MaterialReadDto
            {
                Id=p.Id,
                Instructor=p.InstructorName,
                Name=p.Name,
                DriveLink=p.Drive,
                YoutubeLink=p.Youtube,
                CourseCode =p.CourseCode
            }).ToList();
            int totalCount = await _unitOfWork.MaterialRepo.TotalCount();
            return new MaterialReadPagnationDto
            {
                Materials = materialDto,
                TotalCount = totalCount
            }; 
        }
        public async Task<MaterialReadWithCountDto> GetAllMaterialWithCount()
        {
            var materials = await _unitOfWork.MaterialRepo.GetAllAsync();
            var materialDto = materials.Select(p => new MaterialReadDto
            {
                Id = p.Id,
                Instructor = p.InstructorName,
                Name = p.Name,
                DriveLink = p.Drive,
                YoutubeLink = p.Youtube,
                CourseCode = p.CourseCode
            }).ToList();
            int totalCount = await _unitOfWork.MaterialRepo.TotalCount();
            return new MaterialReadWithCountDto
            {
                Materials = materialDto,
                TotalCount = totalCount
            };
        }

        public async Task<List<MaterialReadDto>> Search(string? Keyword)
        {
            var materials = await _unitOfWork.MaterialRepo.GetAll();
            var materialDto = materials.Select(p => new MaterialReadDto
            {
                Id = p.Id,
                Instructor = p.InstructorName,
                Name = p.Name,
                DriveLink = p.Drive,
                YoutubeLink = p.Youtube,
                CourseCode = p.CourseCode
            }).ToList();
            if (string.IsNullOrWhiteSpace(Keyword)) return materialDto;

            Keyword = Keyword.Trim().ToLower();

            // Step 2: Fuzzy filter in memory
            var results = materialDto
                .Select(m => new
                {
                    Material = m,
                    Score = Fuzz.TokenSetRatio(Keyword, $"{m.Name}")
                })
                .Where(x => x.Score > 30) // You can adjust this threshold
                .OrderByDescending(x => x.Score)
                .Select(x => x.Material)
                .ToList();
            if (!results.Any()) return materialDto;
            return results;
        }

    }
}
