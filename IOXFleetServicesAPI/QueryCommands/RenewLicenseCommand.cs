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
    public class RenewLicenseCommand : IRequest<CustomResponseMessage<bool>>
    {
        [Required(ErrorMessage = "Account Number is required")]
        [StringLength(50, ErrorMessage = "Account Number cannot be longer than 50 characters")]
        public string AccountNumber { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Quote Number is required")]
        [StringLength(50, ErrorMessage = "Quote Number cannot be longer than 50 characters")]
        public string QuoteNumber { get; set; } = string.Empty;

    }
}
