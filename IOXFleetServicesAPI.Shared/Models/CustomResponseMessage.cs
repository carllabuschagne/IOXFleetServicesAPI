using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOXFleetServicesAPI.Shared.Models
{
    public class CustomResponseMessage<T>
    {
        public int MessageCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public CustomResponseMessage()
        {
        }
    }
}
