using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class Parcel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParcelId { get; set; }
        public string? ParcelCode { get; set; }
        public int SenderCustomerId { get; set; }
        public int ReceiverCustomerId { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime ReceiveTime { get; set; }
        [ForeignKey("Branch")]
        public int SenderBranchId { get; set; }
        public virtual Branch? Branchs { get; set; }
        [ForeignKey("Branch")]
        public int ReceiverBranchId { get; set; }
        public DateTime EstimatedReceiveTime { get; set; }
        public bool IsPaid { get; set; }
        [ForeignKey("Van")]
        public int? VanId { get; set; }
        public virtual Van? Vans { get; set; }
        public int? driverId { get; set; }
        [ForeignKey("DeliveryCharge")]
        public int? deliveryChargeId { get; set; }
        public virtual DeliveryCharge? DeliveryCharges { get; set; }
        [ForeignKey("ParcelType")]
        public int? parcelTypeId { get; set; }
        public virtual ParcelType? ParcelTypes { get; set; }
        public decimal price { get; set; }
        public decimal weight { get; set; }
        public string? createBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? updateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool sendingBranch { get; set; }
        public bool percelSendingDestribution { get; set; }
        public bool recebingDistributin { get; set; }
        public bool recebingBranch { get; set; }
        public bool recebingReceber { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Invoice>? Invoices { get; set; }
    }
}
