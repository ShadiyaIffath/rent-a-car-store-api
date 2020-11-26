using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class DMVDto
    {
        public int id { get; set; }

        public string drivingLicense { get; set; }

        public string type { get; set; }

        public DateTime offenseDate { get; set; }
    }
}
