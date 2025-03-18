using StudentGuide.BLL.Dtos.Material;
using StudentGuide.DAL.Repos.MaterialRepo;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.HttpResults;
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
                    YoutubeLink = p.Youtube
                });
            return AllMaterial;
        }
# region GetMaterialBYId
        public async Task<MaterialReadDto?> GetMaterialById(int id)
        {
            Material? MaterialFromDb = await _unitOfWork.MaterialRepo.GetById(id);
            if(MaterialFromDb is null) { return null; }
            MaterialReadDto material = new()
            {
                Id = MaterialFromDb.Id,
                Name = MaterialFromDb.Name,
                Instructor = MaterialFromDb.InstructorName,
                DriveLink = MaterialFromDb.Drive,
                YoutubeLink = MaterialFromDb.Youtube

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
                YoutubeLink = MaterialFromDb.Youtube
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
                    Drive = NewMaterial.Drive,
                    Youtube = NewMaterial.Youtube,
                    InstructorName = NewMaterial.Instructor,
                    CourseCode = NewMaterial.Code
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
        public Task DeleteMaterial(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditMaterial(int id)
        {
            throw new NotImplementedException();
        }
    }
}
