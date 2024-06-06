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
    public class DepositCommand : IRequest<CustomResponseMessage<bool>>
    {
        [Required(ErrorMessage = "Account Number is required")]
        [StringLength(50, ErrorMessage = "Account Number cannot be longer than 50 characters")]
        public string AccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Amount is required")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid total")]
        [Range(1, 999999999999, ErrorMessage = "Total must be between 1 and 999999999999")]
        public double Amount { get; set; }

    }
}
