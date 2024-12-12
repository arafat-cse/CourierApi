using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class BranchStaff
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int branchStaffId { get; set; }
        public string? branchStaffName { get; set; }      
        public string? createBy { get; set; }
        public DateTime? createDate { get; set; }
        public string? updateBy { get; set; }
        public DateTime? updateDate { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Staff")]
        public int staffId { get; set; }
        public virtual Staff? Staffs { get; set; }
    }
}
