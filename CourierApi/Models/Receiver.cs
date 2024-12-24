using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NuGet.Protocol.Plugins;

namespace CourierApi.Models
{
    public class Receiver
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int receiverId {  get; set; }
        public string receiverName { get; set; }
        public string receiverPhoneNumber { get; set; }
        public string ReceiverGmail { get; set; }
        public virtual ICollection<Parcel>? Parcels { get; set; }
    }
}
