using StudentGuide.BLL.Dtos.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.DocumentService
{
   public interface IDocumentService
    {
            public Task<IEnumerable<DocumentReadDto>> GetAllDocument();
            public Task<DocumentReadWithCountDto> GetAllDocumentWithCount();
            public Task<bool> AddNewDocument(DocumentAddDto newDocument);
            public Task<bool> EditDocument(DocumentEditDto document);
            public Task<DocumentReadDto?> GetDocumentById(int id);
            public Task<DocumentReadDto?> GetDocumentBYname(String Name);
            public Task<bool> DeleteDocument(int id);
            public Task<DocumentReadPagnationDto> GetAllDocumentInPagnation(int page, int countPerPage);
            public Task<List<DocumentReadDto>> Search(String Keyword);
}
    }
