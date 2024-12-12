using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class Staff
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int staffId { get; set; }
        public string? staffName { get; set; }
        public string? email { get; set; }       
        public string createBy { get; set; }
        public DateTime createDate { get; set; }
        public DateTime? updateBy { get; set; }
        public DateTime? updateDate { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Designation")]
        public string? designationId { get; set; }
        public virtual Designation Designation { get; set; }

    }
}
