﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class CreateBookingDto
    {
        public VehicleBookingDto vehicleBooking  { get; set; }

        public List<CreateEquipmentBookingDto> equipmentBookings { get; set; }

    }
}
