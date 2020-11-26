using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class DMV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        public string drivingLicense { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        public DateTime offenseDate { get; set; }
    }
}
