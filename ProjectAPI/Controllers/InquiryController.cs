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
    [Route("api/inquiry")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        private IInquiryService _inquiryService;

        public InquiryController(IInquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }
        
        [HttpPost("make-inquiry")]
        public IActionResult MakeInquiry([FromBody] CreateInquiryDto createInquiry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _inquiryService.MakeInquiry(createInquiry);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [Authorize(Roles = "admin")]
        [HttpGet("all-inquiries")]
        public async Task<IActionResult> GetAllInquiry()
        {
            try
            {
                List<InquiryDto> inquiries =  await Task.FromResult(_inquiryService.GetInquiries());
                return Ok(inquiries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete-inquiry")]
        public IActionResult DeleteInquiry(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _inquiryService.DeleteInquiry(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("respond")]
        public IActionResult RespondToInquiry(InquiryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _inquiryService.RespondeToInquiry(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
