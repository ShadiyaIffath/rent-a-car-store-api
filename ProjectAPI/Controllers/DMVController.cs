using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using ProjectAPI.Services.Interfaces;
using Model.Models;

namespace ProjectAPI.Controllers
{
    [Route("api/dmv")]
    [ApiController]
    public class DMVController : Controller
    {
        private IDMVService _dmvService;

        public DMVController(IDMVService dmvService)
        {
            _dmvService = dmvService;
        }

        [HttpGet("get-dmv")]
        public async Task<IActionResult> GetAllDMV()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                List<DMVDto> dtos = await Task.FromResult(_dmvService.GetDMVDtos());
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("validate-license")]
        public async Task<IActionResult> ValidateLicense([FromForm] DMVValidationRequest request )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                bool result = await _dmvService.DMVNotification(request.id, request.drivingLicense);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
