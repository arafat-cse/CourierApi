using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourierApi.Models
{
    public class Designation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int designationId {  get; set; }
        public string Title { get; set; }
        public string? SalaryRange { get; set; }
        public bool IsActive { get; set; } = true;
        public string createBy { get; set; }
        public DateTime createDate { get; set; }
        public string? updateBy { get; set; }
        public DateTime? updateDate { get; set; }
        public virtual ICollection<Staff>? Staffs { get; set; }
    }
}
