using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class CarRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string CarCategory { get; set; }
        [Required]
        public string Model { get; set; }
        public float RatePerMonth { get; set; }
        public float RatePerWeek { get; set; }
        public float Milleage { get; set; }
    }
}
