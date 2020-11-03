using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class UpdateBookingDto
    {
        public UpdateVehicleBookingDto vehicleBooking { get; set; }

        public List<UpdateEquipmentBookingDto> equipmentBookings { get; set; }
    }
}
