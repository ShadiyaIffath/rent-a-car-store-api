using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Models;
using Model.Repositories;
using Model.Repositories.Interfaces;
using Newtonsoft.Json;
using ProjectAPI.Interfaces;
using ProjectAPI.Services.Interfaces;

namespace ProjectAPI.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountsController( IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto logincredentials)
        {
            var token = _accountService.AuthenticateUser(logincredentials);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("signup"), DisableRequestSizeLimit]
        public IActionResult RegisterCustomer([FromBody]CreateCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }        
            try
            {
                if (_accountService.validateEmail(customerDto.email))
                {
                    return Conflict("Email in use");
                }

                if (_accountService.validateLicense(customerDto.licenseId))
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "License is not allowed");
                }
                _accountService.RegisterUser(customerDto);
                 return Ok();
                
            }
            catch (Exception)
            {
                return BadRequest("Invalid driving license or identificcation");
            }          
        }

        [Authorize(Roles = "admin")]
        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<AccountDto> accounts = await Task.FromResult(_accountService.GetAccounts());
            return Ok(accounts);
        }

        [Authorize(Roles ="admin")]
        [HttpDelete("delete-account")]
        public IActionResult DeleteAccountById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _accountService.DeleteAccountById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("update-account-status")]
        public IActionResult UpdateAccountStatus([FromBody]AccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _accountService.UpdateAccountStatus(dto.id, dto.active);
            }
            catch (Exception ex)
            {
                return BadRequest("Update Failed" + ex.Message);
            }
            return Ok();
        }

        [Authorize]
        [HttpGet("get-account")]
        public async Task<IActionResult> GetAccountDetails(int id)
        {
            try
            {
                AccountDto account = await Task.FromResult(_accountService.GetAccountById(id));
                return Ok(account);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("update-account")]
        public IActionResult UpdateAccount([FromBody] AccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_accountService.UpdateAccount(dto))
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update Failed" + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("request-change")]
        public IActionResult RequestPasswordChange([FromBody] AccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string confirmation = _accountService.PasswordConfirmation(dto);
                return Ok(confirmation);
            }
            catch (Exception ex)
            {
                return BadRequest("Update Failed" + ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("update-password")]
        public IActionResult UpdatePassword([FromBody] AccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _accountService.UpdateAccountPassword(dto.id, dto.password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Update Failed" + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("dashboard")]
        public async Task<IActionResult> DashboardData()
        {
            try
            {
                DashboardCardsView ds = await Task.FromResult(_accountService.GetCardDetails());
                return Ok(ds);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
