using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOXFleetServicesAPI.Shared.Models
{
    public class CoreModel
    {
        public Guid CreatedBy { get; set; } = new Guid("8e4c7313-e3cd-4556-be01-56602945c049");

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public Guid? ModifiedBy { get; set; } = null;

        public DateTime? ModifiedDate { get; set; } = null;

        public bool IsDeleted { get; set; } = false;

    }
}
