using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories.Interfaces
{
    public interface IVehicleBookingRepository : IRepositoryBase<VehicleBooking>
    {
        public List<VehicleBooking> validateRange(int? id, DateTime start, DateTime end, int vehicleId);

        public List<VehicleBooking> GetBookings();

        public void DeleteBooking(int id);

        public void UpdateBookingStatus(int id, string status);

        public VehicleBooking GetVehicleBooking(int id);

        public List<VehicleBooking> GetUserBookings(int id);

        public List<VehicleBooking> GetBookingsWithinRange(DateTime start, DateTime end);
    }
}
