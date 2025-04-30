using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.MaterialRepo
{
   public class MaterialRepo : BaseRepo<Material>, IMaterialRepo
    {
        private readonly ApplicationDbContext _context;
        public MaterialRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsInPagnation(int page, int countPerPage)
        {
                if (page < 1) page = 1;
                if (countPerPage < 1) countPerPage = 10;

                var materials = await _context.Materials
                .OrderBy(m => m.Name)
                .Skip((page - 1) * countPerPage)
                .Take(countPerPage).ToListAsync();
                return materials;
           
        }

        public async Task<Material> GetMaterialByNameAsync(string Name)
        {
            return await _context.Materials.FirstAsync(n => n.Name == Name);
        }

        public async Task<int> TotalCount()
        {
            return  await _context.Materials.CountAsync();
        }
    }
    }

