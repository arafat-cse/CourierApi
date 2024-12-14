using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class Designation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int designationId { get; set; }
        public string? DesignationTitle { get; set; }
        public string? Description { get; set; }
        public string? SalaryRange { get; set; }
        public bool IsActive { get; set; } = true;
        public string createBy { get; set; }
        public DateTime createDate { get; set; }
        public string? updateBy { get; set; }
        public DateTime? updateDate { get; set; }
        public ICollection<Staff> StaffMembers { get; set; }
    }
}
