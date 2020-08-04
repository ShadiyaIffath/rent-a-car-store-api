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
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAPI.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }
        // GET: api/<VehicleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VehicleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpGet("getVehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            List<VehicleDto> vehicles = await Task.FromResult(_mapper.Map<List<VehicleDto>>(_vehicleRepository.GetVehicles()));
            return Ok(vehicles);
        }

        [HttpPost("addType")]
        public IActionResult CreateVehicleType([FromBody] CreateVehicleTypeDto vehicleTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VehicleType vehicleType = _mapper.Map<VehicleType>(vehicleTypeDto);
                _vehicleRepository.SaveVehicleType(vehicleType);
            }
            catch (Exception)
            {
                return BadRequest("Invalid details entered");
            }
            return Ok();
        }

        [HttpGet("getTypes")]
        public IActionResult GetVehicleTypes()
        {
            try
            {
                List<VehicleTypeDto> vehicleTypes = _mapper.Map<List<VehicleTypeDto>>(_vehicleRepository.GetVehicleTypes());
                return Ok(vehicleTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("addVehicle"), DisableRequestSizeLimit]
        public IActionResult AddVehicle([FromBody]CreateVehicleDto createVehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Vehicle vehicle = _mapper.Map<Vehicle>(createVehicleDto);
                ImageFile image = JsonConvert.DeserializeObject<ImageFile>(createVehicleDto.image.ToString());
                vehicle.image = Convert.FromBase64String(image.value);
                _vehicleRepository.Create(vehicle);
            }
            catch (Exception)
            {
                return BadRequest("Invalid details entered");
            }
            return Ok();
        }

        [Authorize]
        [HttpPatch("updateVehicle"), DisableRequestSizeLimit]
        public IActionResult UpdateVehicle([FromBody]UpdateVehicleDto updateVehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Vehicle vehicle = _mapper.Map<Vehicle>(updateVehicleDto);
                vehicle.image = Convert.FromBase64String(updateVehicleDto.image.ToString());
                _vehicleRepository.Update(vehicle);
            }
            catch (Exception)
            {
                return BadRequest("Invalid details entered");
            }
            return Ok();
        }
    }
}
