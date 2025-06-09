using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;
using StudentGuide.DAL.Repos.MaterialRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.DocumentRepo
{
   public class DocumentRepo: BaseRepo<Document>, IDocumentRepo
    {
        private readonly ApplicationDbContext _context;
        public DocumentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentssInPagnation(int page, int countPerPage)
        {
            if (page < 1) page = 1;
            if (countPerPage < 1) countPerPage = 10;

            var materials = await _context.Documents
            .OrderBy(m => m.Name)
            .Skip((page - 1) * countPerPage)
            .Take(countPerPage).ToListAsync();
            return materials;
        }

        public async Task<Document> GetDocumentByNameAsync(string Name)
        {
            return await _context.Documents.FirstAsync(n => n.Name == Name);
        }

        public async Task<int> TotalCount()
        {
            return await _context.Documents.CountAsync();
        }
    }
}
