using IOXFleetServicesAPI.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOXFleetServicesAPI.Helpers
{
    public class Validations
    {
        public bool HasSufficientFundsCheck(double AccountAmount, double QuoteAmount)
        {
            return AccountAmount > QuoteAmount ? true : false;
        }

    }
}
