using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.Repos.BaseRepo;

namespace StudentGuide.DAL.Repos.DocumentRepo
{
    public interface IDocumentRepo:IBaseRepo<Document>
    {
        public Task<Document> GetDocumentByNameAsync(String Name);
        public Task<IEnumerable<Document>> GetAllDocumentssInPagnation(int count, int countPerPage);
        public Task<int> TotalCount();
    }
}
