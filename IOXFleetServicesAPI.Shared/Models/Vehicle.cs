using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOXFleetServicesAPI.Shared.Models
{
    public class Vehicle : CoreModel
    {
        [Key]
        public int Id { get; set; }

        public string VIN { get; set; } = string.Empty;

        public string LicenseNumber { get; set; } = string.Empty;

        public string PlateNumber { get; set; } = string.Empty;

        public DateTime? LicenseExpiry { get; set; }

        public string Model { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

    }
}
