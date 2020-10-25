using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Services.Interfaces
{
    public interface IBookingService
    {
        public bool validateVehicleAvailability(int? id, DateTime start, DateTime end, int vehicleId);

        public List<EquipmentDto> GetEquipmentAvailable(int? id, DateTime start, DateTime end);

        public void CreateBooking(CreateBookingDto createBooking);
    }
}
