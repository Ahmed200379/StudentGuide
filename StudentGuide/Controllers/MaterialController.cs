using Microsoft.AspNetCore.Mvc;
using StudentGuide.BLL.Dtos.Material;
using StudentGuide.BLL.Services.Materials;
namespace StudentGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        [Route("GetAllMaterials")]
        public async Task<IActionResult> GetAllMaterials()
        {
            IEnumerable<MaterialReadDto> AllMaterialFromDb = await _materialService.GetAllMaterial();
            if (AllMaterialFromDb is null)
            {
                return BadRequest();
            }
            return Ok(AllMaterialFromDb);
        }
        [HttpPost]
        [Route("AddNewMaterial")]
        public async Task<IActionResult> AddNewMaterial(MaterialAddDto NewMaterial)
        {
            try
            {
                bool isAdded = await _materialService.AddNewMaterial(NewMaterial);
                return isAdded ? NoContent() : BadRequest("Faild to add material.");
            } catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MaterialReadDto>> GetMaterialById(int id)
        {
            MaterialReadDto? material = await _materialService.GetMaterialById(id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }
        [HttpGet]
        [Route("GetMaterialByName/{Name}")]
        public async Task<ActionResult<MaterialReadDto>> GetMaterialByName(String Name)
        {
            MaterialReadDto? material = await _materialService.GetMaterialBYname(Name);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }
    }
}
