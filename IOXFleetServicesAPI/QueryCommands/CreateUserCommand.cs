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
    public class CreateUserCommand : IRequest<CustomResponseMessage<bool>>
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(20, ErrorMessage = "First Name cannot be longer than 20 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(20, ErrorMessage = "Last Name cannot be longer than 20 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID Number is required")]
        [StringLength(20, ErrorMessage = "ID Number cannot be longer than 20 characters")]
        public string IDNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Password cannot be longer than 20 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account Number is required")]
        [StringLength(50, ErrorMessage = "Account Number cannot be longer than 50 characters")]
        public string AccountNumber { get; set; } = string.Empty;

    }
}
