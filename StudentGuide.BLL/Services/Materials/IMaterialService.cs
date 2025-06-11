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
        public Task<IEnumerable<DocumentReadDto>> GetAllMaterial();
        public Task<MaterialReadWithCountDto> GetAllMaterialWithCount();
        public Task<bool> AddNewMaterial(DocumentAddDto newmaterial);
        public Task<bool> EditMaterial(DocumentEditDto material);
        public Task<DocumentReadDto?> GetMaterialById(int id);
        public Task<DocumentReadDto?> GetMaterialBYname(String Name);
        public Task<bool> DeleteMaterial(int id);
        public Task<DocumentReadPagnationDto> GetAllMaterialInPagnation(int page, int countPerPage);
        public Task<List<DocumentReadDto>> Search(String Keyword);
    }
}
