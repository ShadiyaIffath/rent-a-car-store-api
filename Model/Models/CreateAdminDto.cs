using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class CreateAdminDto
    {
        public string email { get; set; }

        public string password { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime dob { get; set; }

        public int phone { get; set; }

        public bool active { get; set; }

        public DateTime activatedDate { get; set; }
    }
}
