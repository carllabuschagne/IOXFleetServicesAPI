using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOXFleetServicesAPI.Shared.Models
{
    public class Quote : CoreModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? ValidTo { get; set; }

        public string QuoteNumber { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Amount { get; set; }

        public string Status { get; set; } = string.Empty;

        public Vehicle vehicle { get; set; }
    }
}
