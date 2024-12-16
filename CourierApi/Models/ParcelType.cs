using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class ParcelType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int parcelTypeId { get; set; }
        public string parcelTypeName { get; set; }
        public string createBy { get; set; }
        public DateTime createDate { get; set; }
        public string? updateBy { get; set; }
        public DateTime? updateDate { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Parcel>? Parcels { get; set; }
        public virtual ICollection<DeliveryCharge>? DeliveryCharges { get; set; }
    }
}
