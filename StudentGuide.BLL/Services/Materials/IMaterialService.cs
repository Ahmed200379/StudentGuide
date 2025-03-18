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
        public Task EditMaterial(int id);
        public Task<MaterialReadDto?> GetMaterialById(int id);
        public Task<MaterialReadDto?> GetMaterialBYname(String Name);
        public Task DeleteMaterial(int id);
        
    }
}
