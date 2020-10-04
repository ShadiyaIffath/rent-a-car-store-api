using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using ProjectAPI.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAPI.Controllers
{
    [Route("api/equipment")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }


        [Authorize]
        [HttpPost("add-equipment"), DisableRequestSizeLimit]
        public IActionResult CreateEquipment([FromBody] CreateEquipmentDto createEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool createStatus = _equipmentService.CreateEquipment(createEquipment);
                if (!createStatus)
                {
                    return Conflict("Name already in use");
                }
            }
            catch (Exception)
            {
                return BadRequest("Failed");
            }
            return Ok();
        }

        [HttpPost("add-category"), DisableRequestSizeLimit]
        public IActionResult CreateEquipmentCategory([FromBody] CreateCategoryDto createCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _equipmentService.CreateEquipmentCategory(createCategory);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed");
            }
            return Ok();
        }



        [HttpGet()]
        public async Task<IActionResult> GetAllEquipment()
        {
            List<EquipmentDto> equipment = await Task.FromResult(_equipmentService.GetEquipment());
            return Ok(equipment);
        }

        [HttpGet("equipment-categories")]
        public async Task<IActionResult> GetAllEquipmentCategories()
        {
            List<EquipmentCategoryDto> categories = await Task.FromResult(_equipmentService.GetEquipmentCategories());
            return Ok(categories);
        }

    }
}
