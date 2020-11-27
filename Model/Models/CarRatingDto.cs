using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class CarRatingDto
    {
        public int id { get; set; }

        public string CarCategory { get; set; }

        public string Model { get; set; }
        public float RatePerMonth { get; set; }
        public float RatePerWeek { get; set; }
        public float Milleage { get; set; }
    }
}
