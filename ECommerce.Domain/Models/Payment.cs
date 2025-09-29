using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Payment : BaseEntity
    {
        // FK
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty; // e.g. "CreditCard", "PayPal"
        public string Status { get; set; } = "Pending";
    }

}
