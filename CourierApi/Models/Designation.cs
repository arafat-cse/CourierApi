using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourierApi.Models
{
    public class Designation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int designationId {  get; set; }
        public string Title { get; set; }
        public virtual ICollection<Staff>? Staffs { get; set; }
    }
}
