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
    public class GetVehicleListCommand : IRequest<CustomResponseMessage<List<Vehicle>>>
    {
        [Required(ErrorMessage = "Account Number is required")]
        [StringLength(50, ErrorMessage = "Account Number cannot be longer than 50 characters")]
        public string AccountNumber { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Filter cannot be longer than 20 characters")]
        public string Filter { get; set; } = string.Empty;

        [Required(ErrorMessage = "Page Number is required")]
        [Range(1, 999999999999, ErrorMessage = "Total must be between 1 and 999999999999")]
        public int PageNumber { get; set; } = 1;

        [Required(ErrorMessage = "Page Size is required")]
        [Range(1, 999999999999, ErrorMessage = "Total must be between 1 and 999999999999")]
        public int PageSize { get; set; } = 10;

    }
}
