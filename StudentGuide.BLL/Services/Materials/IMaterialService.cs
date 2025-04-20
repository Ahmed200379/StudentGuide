using StudentGuide.BLL.Dtos.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.Materials
{
   public interface IMaterialService
    {
        public Task<IEnumerable<MaterialReadDto>> GetAllMaterial();
        public Task<bool> AddNewMaterial(MaterialAddDto newmaterial);
        public Task<bool> EditMaterial(MaterialEditDto material);
        public Task<MaterialReadDto?> GetMaterialById(int id);
        public Task<MaterialReadDto?> GetMaterialBYname(String Name);
        public Task<bool> DeleteMaterial(int id);
        public Task<MaterialReadPagnationDto> GetAllMaterialInPagnation(int page, int countPerPage);
        public Task<List<MaterialReadDto>> Search(String Keyword);
    }
}
