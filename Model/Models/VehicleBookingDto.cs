﻿using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class VehicleBookingDto
    {
        public int id { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public string confirmationCode { get; set; }

        public double totalCost { get; set; }

        public DateTime createdOn { get; set; }

        public string status { get; set; }

        public int vehicleId { get; set; }

        public VehicleDto vehicle { get; set; }

        public int accountId { get; set; }

        public AccountDto account { get; set; }

    }
}
