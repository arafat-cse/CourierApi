using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int customerId { get; set; }
        public string? customerName { get; set; }
        public string? customerEmail { get; set; }
        public string? customerMobile { get; set; }
        public string? address { get; set; }
        public string? createBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? updateBy { get; set; }
        public DateTime updateDate { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Invoice>? Invoices { get; set; }
    }
}
