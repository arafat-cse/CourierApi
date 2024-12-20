using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class Parcel
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int parcelId { get; set; }
        //public string parcelCode { get; set; }
        //public int senderCustomerId { get; set; }
        //public int receiverCustomerId { get; set; }
        //public DateTime? sendTime { get; set; }
        //public DateTime? receiveTime { get; set; }
        //public int senderBranchId { get; set; }
        //public DateTime estimatedReceiveTime { get; set; }
        //public bool IsPaid { get; set; }       
        //public decimal price { get; set; }
        //public string createBy { get; set; }
        //public DateTime? createDate { get; set; }
        //public string? updateBy { get; set; }
        //public DateTime? updateDate { get; set; }
        //public bool sendingBranch { get; set; }
        //public bool percelSendingDestribution { get; set; }
        //public bool recebingDistributin { get; set; }
        //public bool recebingBranch { get; set; }
        //public bool recebingReceber { get; set; }
        //[ForeignKey("Branch")]
        //public int receiverBranchId { get; set; }
        //public virtual Branch? Branchs { get; set; }

        //public bool IsActive { get; set; }
        //[ForeignKey("Van")]
        //public int? VanId { get; set; }
        //public virtual Van? Vans { get; set; }
        //public int? driverId { get; set; }
        //[ForeignKey("DeliveryCharge")]
        //public int? deliveryChargeId { get; set; }
        //public virtual DeliveryCharge? DeliveryCharges { get; set; }
        //[ForeignKey("ParcelType")]
        //public int? parcelTypeId { get; set; }
        //public virtual ParcelType? ParcelTypes { get; set; }
        //public virtual ICollection<Invoice>? Invoices { get; set; }


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int parcelId { get; set; }
        public string parcelCode { get; set; }

        public int senderCustomerId { get; set; }
        public int receiverCustomerId { get; set; }
        public DateTime? sendTime { get; set; }
        public DateTime? receiveTime { get; set; }

        [ForeignKey("Branch")]
        public int senderBranchId { get; set; }
        public virtual Branch SenderBranch { get; set; }  

        [ForeignKey("Branch")]
        public int receiverBranchId { get; set; }
        public virtual Branch ReceiverBranch { get; set; }

        public DateTime estimatedReceiveTime { get; set; }
        public bool IsPaid { get; set; }
        public decimal price { get; set; } 

        public string createBy { get; set; }
        public DateTime? createDate { get; set; }
        public string updateBy { get; set; }
        public DateTime? updateDate { get; set; }

        public bool isDispatchedFromBranch { get; set; }
        public bool isInTransit { get; set; }
        public bool isReceivedAtBranch { get; set; }
        public bool isReceivedByReceiver { get; set; }

        [ForeignKey("Van")]
        public int? vanId { get; set; }
        public virtual Van Van { get; set; }

        public int? driverId { get; set; }

        [ForeignKey("DeliveryCharge")]
        public int? deliveryChargeId { get; set; }
        public virtual DeliveryCharge DeliveryCharge { get; set; }

        [ForeignKey("ParcelType")]
        public int? parcelTypeId { get; set; }
        public virtual ParcelType ParcelType { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public bool IsActive { get; set; } = true;
    

    }
}
