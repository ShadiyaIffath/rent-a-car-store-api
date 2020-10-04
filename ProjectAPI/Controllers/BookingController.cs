using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Models;
using Model.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAPI.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        // GET: api/<BookingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookingController>
        [HttpPost("createBooking")]
        public IActionResult Post([FromBody] CreateBookingDto createBookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VehicleBooking vehicleType = _mapper.Map<VehicleBooking>(createBookingDto);
            }
            catch (Exception)
            {
                return BadRequest("Invalid details entered");
            }
            return Ok();
        }

    }
}
