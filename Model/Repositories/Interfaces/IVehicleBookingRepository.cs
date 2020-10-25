using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.Interfaces
{
    public interface IVehicleBookingRepository : IRepositoryBase<VehicleBooking>
    {
        public List<VehicleBooking> validateRange(int? id, DateTime start, DateTime end, int vehicleId);
    }
}
