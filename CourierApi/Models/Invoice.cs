using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class Invoice
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int invoiceId { get; set; }
        public DateTime paymentTime { get; set; }
        public decimal amount { get; set; }
        public string? particular { get; set; }       
        public string createBy { get; set; }
        public DateTime createDate { get; set; }
        public string? updateBy { get; set; }
        public DateTime? updateDate { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }
        public virtual Customer? Customers { get; set; }
        [ForeignKey("paymentMethod")]
        public int paymentMethodId { get; set; }
        public virtual PaymentMethod? PaymentMethods { get; set; }
        [ForeignKey("Parcel")]
        public int ParcelsId { get; set; }
        public virtual Parcel? Parcels { get; set; }
    }
}
