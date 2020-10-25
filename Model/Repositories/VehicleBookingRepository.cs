using Microsoft.Extensions.Logging;
using Model.DatabaseContext;
using Model.Entities;
using Model.Repositories.Base;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Repositories
{
    public class VehicleBookingRepository : RepositoryBase<VehicleBooking>, IVehicleBookingRepository
    {
        private ILogger _logger;

        public VehicleBookingRepository(ClientDbContext clientDbContext, ILogger<VehicleBookingRepository> logger) : base(clientDbContext)
        {
            _logger = logger;
        }

        public List<VehicleBooking> validateRange(int? id, DateTime start, DateTime end, int vehicleId)
        {
            List<VehicleBooking> bookings = new List<VehicleBooking>();
            //new booking duration validation
            if (id == 0)
            {
                bookings = _clientDbContext.VehicleBookings.Where(x => x.vehicle.id == vehicleId && x.status=="Confirmed" && ((x.startTime <= start && x.endTime >= start) || (x.startTime <= end && x.endTime >= end))).ToList();
            }//existing booking duration validation
            else
            {
                bookings = _clientDbContext.VehicleBookings.Where(x => x.vehicle.id == vehicleId && x.id != id && x.status == "Confirmed" && ((x.startTime <= start && x.endTime >= start) || (x.startTime <= end && x.endTime >= end))).ToList();
            }
            return bookings;
        }
    }

}
