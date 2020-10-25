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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAPI.Controllers
{
    [Authorize]
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("validate-booking")]
        public IActionResult ValidateBookingRange([FromBody] BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if(_bookingService.validateVehicleAvailability(bookingDto.id,bookingDto.startTime, bookingDto.endTime, bookingDto.vehicleId)== true)
                {
                    return Ok();
                }
                return Conflict("Invalid range");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("get-available-equipment")]
        public IActionResult GetAvailableEquipment([FromBody] BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<EquipmentDto> equipment = _bookingService.GetEquipmentAvailable(bookingDto.id, bookingDto.startTime, bookingDto.endTime);

                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-booking")]
        public IActionResult CreateBooking([FromBody]CreateBookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _bookingService.CreateBooking(bookingDto);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
