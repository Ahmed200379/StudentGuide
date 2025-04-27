using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using StudentGuide.BLL.Dtos.Material;
using StudentGuide.BLL.Services.HashId;
using StudentGuide.BLL.Services.Materials;
namespace StudentGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly HashIdService _hashIdService;
        public MaterialController(IMaterialService materialService, HashIdService hashIdService)
        {
            _materialService = materialService;
            _hashIdService = hashIdService;
        }

        [HttpGet]
        [Route("GetAllMaterials")]
        public async Task<IActionResult> GetAllMaterials()
        {
            try
            {
                IEnumerable<MaterialReadDto> AllMaterialFromDb = await _materialService.GetAllMaterial();
                if (AllMaterialFromDb is null)
                {
                    return BadRequest();
                }
                return Ok(AllMaterialFromDb);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MaterialReadDto>> GetMaterialById(int id)
        {
            try
            {
                MaterialReadDto? material = await _materialService.GetMaterialById(id);
                if (material == null)
                {
                    return NotFound();
                }
                return Ok(material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [HttpGet]
        [Route("GetMaterialByName/{Name}")]
        public async Task<ActionResult<MaterialReadDto>> GetMaterialByName(String Name)
        {
            try
            {
                MaterialReadDto? material = await _materialService.GetMaterialBYname(Name);
                if (material == null )
                {
                    return NotFound();
                }
                return Ok(material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            
        }
        [HttpGet]
        [Route("GetAllMaterialsInPagnation/{page}/{countPerPage}")]
        public async Task<IActionResult> GetAllMaterialsInPagnation(int page, int countPerPage)
        {
            try
            {
                var materials = await _materialService.GetAllMaterialInPagnation(page, countPerPage);
                if (materials == null)
                    return NotFound();
                else
                    return Ok(materials);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }

        }
        [HttpPost]
        [Route("Dashboard/AddNewMaterial")]
        public async Task<IActionResult> AddNewMaterial(MaterialAddDto NewMaterial)
        {
            if (!string.IsNullOrWhiteSpace(NewMaterial.Instructor) && !string.IsNullOrWhiteSpace(NewMaterial.Name))
            {
                try
                {
                    bool isAdded = await _materialService.AddNewMaterial(NewMaterial);
                    return isAdded ? NoContent() : BadRequest("Faild to add material.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
            }
            else
            {
                return BadRequest("eaither instructor or name can not be null");
            }
           

        }
        [HttpDelete]
        [Route("Dashboard/DeleteMaterial/{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {

            try
            {
                bool isDeleted = await _materialService.DeleteMaterial(id);
                return isDeleted ? NoContent() : BadRequest("Failed to delete material.");
            }catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
           
        }
        [HttpPut]
        [Route("Dashboard/UpdateMaterial")]
        public async Task<IActionResult> UpdateMaterial([FromBody]MaterialEditDto materialEditDto)
        {
            if (!string.IsNullOrWhiteSpace(materialEditDto.Instructor) && !string.IsNullOrWhiteSpace(materialEditDto.Name))
            {
                try
                {
                    bool isUpdated = await _materialService.EditMaterial(materialEditDto);
                    if (isUpdated)
                        return NoContent();
                    else
                        return BadRequest("Faild to Update");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
            }
            else
            {
                return BadRequest("eaither instructor or name can not be null");
            }
        }
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery]String keyword)
        {
                try
                {
                  var materials= await _materialService.Search(keyword);
                 return Ok(materials);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
        }
    }
    }


