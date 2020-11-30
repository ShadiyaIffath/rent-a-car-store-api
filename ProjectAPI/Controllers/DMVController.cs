using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using ProjectAPI.Services.Interfaces;

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
        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin,customer")]
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
