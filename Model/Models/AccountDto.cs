using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class AccountDto
    {
        public int id { get; set; }

        public string email { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime dob { get; set; }

        public int phone { get; set; }

        public bool active { get; set; }

        public DateTime activatedDate { get; set; }

        public int typeId { get; set; }

        public byte[] drivingLicense { get; set; }

        public byte[] additionalIdentitfication { get; set; }
    }
}
