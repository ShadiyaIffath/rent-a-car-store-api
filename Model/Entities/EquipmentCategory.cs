using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class EquipmentCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(25)]
        public string title { get; set; }

        [MaxLength(200)]
        public string description { get; set; }

        [Required]
        public byte[] image { get; set; }

    }
}
