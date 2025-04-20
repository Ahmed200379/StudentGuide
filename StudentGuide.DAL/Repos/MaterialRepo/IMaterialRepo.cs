using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.MaterialRepo
{
   public interface IMaterialRepo:IBaseRepo<Material>
    {
        public Task<Material> GetMaterialByNameAsync(String Name);
        public Task<IEnumerable<Material>> GetAllMaterialsInPagnation(int count, int countPerPage);
        public Task<int> TotalCount();
    }
}
