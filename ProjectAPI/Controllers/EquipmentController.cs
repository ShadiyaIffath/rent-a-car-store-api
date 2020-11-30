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


        [Authorize(Roles = "admin")]
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
            catch (Exception ex)
            {
                return BadRequest("Failed: "+ ex.Message);
            }
            return Ok();
        }

        [Authorize(Roles = "admin")]
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
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("update-equipment"), DisableRequestSizeLimit]
        public IActionResult UpdateEquipment([FromBody] EquipmentDto equipmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool createStatus = _equipmentService.UpdateEquipment(equipmentDto);
                if (!createStatus)
                {
                    return Conflict("Name already in use");
                }
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet()]
        public async Task<IActionResult> GetAllEquipment()
        {
            List<EquipmentDto> equipment = await Task.FromResult(_equipmentService.GetEquipment());
            return Ok(equipment);
        }

        [Authorize(Roles = "admin,customer")]
        [HttpGet("equipment-categories")]
        public async Task<IActionResult> GetAllEquipmentCategories()
        {
            List<EquipmentCategoryDto> categories = await Task.FromResult(_equipmentService.GetEquipmentCategories());
            return Ok(categories);
        }

        [Authorize(Roles = "admin,customer")]
        [HttpGet("get-equipment")]
        public async Task<IActionResult> GetEquipmentById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                EquipmentDto dto = await Task.FromResult(_equipmentService.GetEquipmentById(id));
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete-equipment")]
        public IActionResult DeleteEquipmentById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
               _equipmentService.DeleteEquipment(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
