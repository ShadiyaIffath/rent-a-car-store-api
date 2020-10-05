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
                if (_accountService.RegisterUser(customerDto))
                {
                    return Ok();
                }
                else
                {
                    return Conflict("Email in use");
                }
            }
            catch (Exception)
            {
                return BadRequest("Invalid driving license or identificcation");
            }          
        }

        [Authorize]
        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<AccountDto> accounts = await Task.FromResult(_accountService.GetAccounts());
            return Ok(accounts);
        }

        [Authorize]
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
    }
}
