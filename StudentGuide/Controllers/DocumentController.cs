using Microsoft.AspNetCore.Mvc;
using StudentGuide.BLL.Dtos.Document;
using StudentGuide.BLL.Services.DocumentService;

[Route("api/[controller]")]
[ApiController]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var documents = await _documentService.GetAllDocument();
        return Ok(documents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var document = await _documentService.GetDocumentById(id);
        return document == null ? NotFound() : Ok(document);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] DocumentAddDto dto)
    {
        var result = await _documentService.AddNewDocument(dto);
        return result ? Ok("Document added") : BadRequest("Failed to add");
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] DocumentEditDto dto)
    {
        var result = await _documentService.EditDocument(dto);
        return result ? Ok("Document updated") : NotFound("Document not found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _documentService.DeleteDocument(id);
        return result ? Ok("Document deleted") : NotFound("Document not found");
    }
}
