using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Model.Entities;
using MimeKit.Cryptography;
using ProjectAPI.Services.Interfaces;

namespace ProjectAPI.Controllers
{
    [Authorize]
    [Route("/api/insurance")]
    [ApiController]
    public class InsuranceController : Controller
    {
        private IInsurerService _insurerService;

        public InsuranceController(IInsurerService insurerService)
        {
            _insurerService = insurerService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("frauds")]
        public async Task<IActionResult> GetAllFrauds()
        {
            try
            {
                List<FraudClaim> competitors = await Task.FromResult(_insurerService.GetFraudClaims());
                return Ok(competitors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,customer")]
        [HttpPost("validate-fraud")]
        public async Task<IActionResult> ValidateLicense(string drivingLicense)
        {
            try
            {
                bool exists = await Task.FromResult(_insurerService.ValidateLicenseExists(drivingLicense));
                return Ok(exists);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
