using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class UpdateStatusVehicleDto
    {
        public int id { get; set; }

        public DateTime dayRemoved { get; set; }

        public bool active { get; set; }
    }
}
