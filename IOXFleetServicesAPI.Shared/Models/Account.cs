using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOXFleetServicesAPI.Shared.Models
{
    public class Account : CoreModel
    {
        [Key]
        public int Id { get; set; }

        public string AccountNumber { get; set; } = string.Empty;

        public double TotalAmount { get; set; }

        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public List<Quote> Quotes { get; set; } = new List<Quote>();

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public User User { get; set; }

    }
}
