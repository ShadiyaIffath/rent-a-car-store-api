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

        public List<BookingDto> GetAllBookings();

        public void DeleteBooking(int id);

        public bool ValidateBooking(UpdateBookingDto dto);

        public void UpdateBooking(UpdateBookingDto dto);

        public void UpdateBookingStatus(int bookingId, string status);

        public BookingDto GetBooking(int id);

        public List<BookingDto> GetUserBookings(int id);
    }
}
