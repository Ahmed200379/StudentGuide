using StudentGuide.BLL.Dtos.Document;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.DocumentService
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddNewDocument(DocumentAddDto newDocument)
        {
            var document = new Document
            {
                Name = newDocument.NameOfDocument,
                Link = newDocument.LinkOfDocument
            };

            await _unitOfWork.DocumentRepo.AddAsync(document);
            int saved = await _unitOfWork.Complete();
            return saved > 0;
        }

        public async Task<bool> DeleteDocument(int id)
        {
            var doc = await _unitOfWork.DocumentRepo.GetByIdAsync(id);
            if (doc == null) return false;

            await _unitOfWork.DocumentRepo.Delete(doc);
            int saved = await _unitOfWork.Complete();
            return saved > 0;
        }

        public async Task<bool> EditDocument(DocumentEditDto dto)
        {
            var doc = await _unitOfWork.DocumentRepo.GetByIdAsync(dto.Id);
            if (doc == null) return false;

            doc.Name = dto.NameOfDocument;
            doc.Link = dto.LinkOfDocument;

            await _unitOfWork.DocumentRepo.Update(doc);
            int updated = await _unitOfWork.Complete();
            return updated > 0;
        }

        public async Task<IEnumerable<DocumentReadDto>> GetAllDocument()
        {
            IEnumerable<Document> allDocuments = await _unitOfWork.DocumentRepo.GetAll();

            return allDocuments.Select(p => new DocumentReadDto
            {
                NameOfDocument = p.Name,
                LinkOfDocument = p.Link
            });
        }

        public async Task<DocumentReadPagnationDto> GetAllDocumentInPagnation(int page, int countPerPage)
        {
            var docs = await _unitOfWork.DocumentRepo.GetAllDocumentssInPagnation(page, countPerPage);
            var total = await _unitOfWork.DocumentRepo.TotalCount();

            return new DocumentReadPagnationDto
            {
                Documents = docs.Select(d => new DocumentReadDto
                {
                    NameOfDocument = d.Name,
                    LinkOfDocument = d.Link
                }).ToList(),
                TotalCount = total
            };
        }

        public async Task<DocumentReadWithCountDto> GetAllDocumentWithCount()
        {
            var docs = await _unitOfWork.DocumentRepo.GetAll();
            return new DocumentReadWithCountDto
            {
                Documents = docs.Select(d => new DocumentReadDto
                {
                    NameOfDocument = d.Name,
                    LinkOfDocument = d.Link
                }).ToList(),
                TotalCount = docs.Count()
            };
        }

        public async Task<DocumentReadDto?> GetDocumentById(int id)
        {
            var doc = await _unitOfWork.DocumentRepo.GetByIdAsync(id);
            if (doc == null) return null;

            return new DocumentReadDto
            {
                NameOfDocument = doc.Name,
                LinkOfDocument = doc.Link
            };
        }

        public async Task<DocumentReadDto?> GetDocumentBYname(string name)
        {
            var doc = await _unitOfWork.DocumentRepo.GetDocumentByNameAsync(name);
            if (doc == null) return null;

            return new DocumentReadDto
            {
                NameOfDocument = doc.Name,
                LinkOfDocument = doc.Link
            };
        }

        public async Task<List<DocumentReadDto>> Search(string keyword)
        {
            var docs = await _unitOfWork.DocumentRepo.GetAll();
            var all = docs.Select(d => new DocumentReadDto
            {
                NameOfDocument = d.Name,
                LinkOfDocument = d.Link
            }).ToList();

            if (string.IsNullOrWhiteSpace(keyword)) return all;

            keyword = keyword.ToLower().Trim();
            return all.Where(d => d.NameOfDocument.ToLower().Contains(keyword)).ToList();
        }
    }
}
