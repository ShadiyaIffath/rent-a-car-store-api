using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class UpdateVehicleDto
    {
        public int id { get; set; }

        public string model { get; set; }

        public string engine { get; set; }

        public string make { get; set; }

        public bool active { get; set; }

        public bool automatic { get; set; }

        public DateTime dayAdded { get; set; }

        public DateTime dayRemoved { get; set; }

        public int typeId { get; set; }

        public Object image { get; set; }

        public float engineCapacity { get; set; }

        public float fuelConsumption { get; set; }

    }
}
