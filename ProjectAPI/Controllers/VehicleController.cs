using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Models;
using Model.Repositories.Interfaces;
using ProjectAPI.Services.Interfaces;

namespace ProjectAPI.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("get-type")]
        public async Task<IActionResult> GetTypeById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                VehicleTypeDto vehicleType = await Task.FromResult(_vehicleService.GetVehicleTypeById(id));
                return Ok(vehicleType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-vehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            List<VehicleDto> vehicles = await Task.FromResult(_vehicleService.GetAllVehicles());
            return Ok(vehicles);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add-type")]
        public IActionResult CreateVehicleType([FromBody] CreateVehicleTypeDto vehicleTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _vehicleService.CreateVehicleType(vehicleTypeDto);
            }
            catch (Exception)
            {
                return BadRequest("Invalid details entered");
            }
            return Ok();
        }

        [HttpGet("get-types")]
        public async Task<IActionResult> GetVehicleTypes()
        {
            try
            {
                List<VehicleTypeDto> vehicleTypes = await Task.FromResult(_vehicleService.GetVehicleTypes());
                return Ok(vehicleTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add-vehicle"), DisableRequestSizeLimit]
        public IActionResult AddVehicle([FromBody]CreateVehicleDto createVehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _vehicleService.AddVehicle(createVehicleDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid details entered: "+ ex.Message);
            }
            return Ok();
        }

        [HttpGet("get-vehicle")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                VehicleDto vehicle = await Task.FromResult(_vehicleService.GetVehicleById(id));
                return Ok(vehicle);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("update-vehicle"), DisableRequestSizeLimit]
        public IActionResult UpdateVehicle([FromBody]UpdateVehicleDto updateVehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _vehicleService.UpdateVehicle(updateVehicleDto);
            }
            catch (Exception)
            {
                return BadRequest("Invalid details entered");
            }
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("update-vehicle-status")]
        public IActionResult UpdateVehicleStatus([FromBody] UpdateStatusVehicleDto updateVehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _vehicleService.UpdateVehicleStatus(updateVehicleDto);
            }
            catch (Exception)
            {
                return BadRequest("Invalid details entered");
            }
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete-vehicle")]
        public IActionResult DeleteVehicleById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _vehicleService.DeleteVehicleById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
