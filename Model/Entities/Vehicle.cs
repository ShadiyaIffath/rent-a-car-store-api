using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public bool active { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime dayAdded { get; set; }

        [DataType(DataType.Date)]
        public DateTime dayRemoved { get; set; }

        [Required]
        [MaxLength(10)]
        public string carCode { get; set; }


        [Required]
        [MaxLength(25)]
        public string model { get; set; }

        [Required]
        [MaxLength(10)]
        public string engine { get; set; }

        [Required]
        public bool automatic { get; set; }

        [Required]
        public byte[] image { get; set; }

        [Required]
        public int fuelConsumption { get; set; }

        [Required]
        public int engineCapacity { get; set; }

        [ForeignKey("typeId")]
        public VehicleType type { get; set; }

        public int typeId { get; set; }
    }
}
