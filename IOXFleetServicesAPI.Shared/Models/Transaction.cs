using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOXFleetServicesAPI.Shared.Models
{
    public class Transaction : CoreModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public string Type { get; set; } = string.Empty;

        public double Amount { get; set; }

    }
}
