using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class UpdateAccountDto
    {
        public int id { get; set; }
        public string licenseId { get; set; }
        public Object additionalIdentification { get; set; }
        public Object drivingLicense { get; set; }
    }
}
