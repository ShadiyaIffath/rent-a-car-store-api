using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Model.Models;
using ProjectAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Model.Repositories.Interfaces;
using Model.Entities;

namespace ProjectAPI.Controllers
{
    [Authorize]
    [Route("/api/competitors")]
    [ApiController]
    public class WebScrapingController : Controller
    {

        private IWebScrapingService _webScrapingService;
        private IFraudClaimRepository _fraudClaimRepository;
        public WebScrapingController(IWebScrapingService webScrapingService, IFraudClaimRepository fraudClaimRepository)
        {
            _webScrapingService = webScrapingService;
            _fraudClaimRepository = fraudClaimRepository;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllCompetitiveData()
        {
            try
            {
                List<CarRatingDto> competitors = await Task.FromResult(_webScrapingService.GetRatingDtos());
                return Ok(competitors);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
   }
}
