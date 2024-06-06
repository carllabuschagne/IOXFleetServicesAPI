using IOXFleetServicesAPI.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOXFleetServicesAPI.QueryCommands
{
    public class AddVehicleCommand : IRequest<CustomResponseMessage<bool>>
    {
        [Required(ErrorMessage = "Account Number is required")]
        [StringLength(50, ErrorMessage = "Account Number cannot be longer than 50 characters")]
        public string AccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "VIN number is required")]
        [StringLength(50, ErrorMessage = "VIN number cannot be longer than 50 characters")]
        public string VIN { get; set; } = string.Empty;

        [Required(ErrorMessage = "License Number is required")]
        [StringLength(50, ErrorMessage = "License Number cannot be longer than 50 characters")]
        public string LicenseNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Plate Number is required")]
        [StringLength(20, ErrorMessage = "Plate Number cannot be longer than 20 characters")]
        public string PlateNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "License Expiry is required")]
        public DateTime? LicenseExpiry { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [StringLength(20, ErrorMessage = "Model cannot be longer than 20 characters")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Color is required")]
        [StringLength(10, ErrorMessage = "Color cannot be longer than 10 characters")]
        public string Color { get; set; } = string.Empty;
    }
}
