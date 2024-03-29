﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class UpdateVehicleBookingDto
    {
        public int id { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public string confirmationCode { get; set; }

        public double totalCost { get; set; }

        public DateTime createdOn { get; set; }

        public string status { get; set; }

        public int vehicleId { get; set; }

        public int accountId { get; set; }
    }
}
