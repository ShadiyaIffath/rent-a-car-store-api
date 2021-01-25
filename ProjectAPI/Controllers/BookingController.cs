using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Enums;
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

        [Authorize(Roles = "admin,customer")]
        [HttpPost("validate-booking")]
        public async Task<IActionResult> ValidateBookingRange([FromBody] VehicleBookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool valid = await Task.FromResult(_bookingService.validateVehicleAvailability(bookingDto.id,
                    bookingDto.startTime, bookingDto.endTime, bookingDto.vehicleId));
                if (valid == true)
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

        [Authorize(Roles = "admin,customer")]
        [HttpPost("get-available-equipment")]
        public async Task<IActionResult> GetAvailableEquipment([FromBody] VehicleBookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<EquipmentDto> equipment = await Task.FromResult(
                    _bookingService.GetEquipmentAvailable(bookingDto.id, bookingDto.startTime, bookingDto.endTime));

                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,customer")]
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

        [Authorize(Roles = "admin")]
        [HttpGet("all-bookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                return Ok(await Task.FromResult(_bookingService.GetAllBookings()));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,customer")]
        [HttpDelete("delete-booking")]
        public IActionResult DeleteBooking(int id)
        {
            try
            {
                _bookingService.DeleteBooking(id);
                return Ok();

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,customer")]
        [HttpPatch("update-booking")]
        public IActionResult UpdateBooking([FromBody] UpdateBookingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (dto.vehicleBooking.status != "Confirmed")
                {
                    if (dto.vehicleBooking.status != "Collected")
                    {
                        _bookingService.UpdateBookingStatus(dto.vehicleBooking.id, dto.vehicleBooking.status);
                        return Ok();
                    }
                }
                if(_bookingService.ValidateBooking(dto) == false)
                {
                    return Conflict();
                }

                _bookingService.UpdateBooking(dto);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest("Failed:" + ex.Message);
            }
        }

        [Authorize(Roles = "admin,customer")]
        [HttpPatch("update-status")]
        public IActionResult UpdateStatus([FromBody] UpdateBookingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (dto.vehicleBooking.status != "Confirmed")
                {
                    if (dto.vehicleBooking.status != "Collected")
                    {
                        _bookingService.UpdateBookingStatus(dto.vehicleBooking.id, dto.vehicleBooking.status);
                        return Ok();
                    }
                }
                if (_bookingService.ValidateBooking(dto) == false)
                {
                    return Conflict();
                }
                _bookingService.UpdateBooking(dto);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,customer")]
        [HttpGet("get-booking")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                BookingDto booking = await Task.FromResult(_bookingService.GetBooking(id));
                return Ok(booking);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "customer")]
        [HttpGet("user-bookings")]
        public async Task<IActionResult> GetUserBookings(int id)
        {
            try
            {
                List<BookingDto> bookings = await Task.FromResult(_bookingService.GetUserBookings(id));
                return Ok(bookings);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
