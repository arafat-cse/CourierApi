using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class Branch
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int branchId { get; set; }
        public string? branchName { get; set; }
        public string? address { get; set; }
        public int? ParentId { get; set; } // Nullable for root branches
        public Branch ParentBranch { get; set; } // Navigation property for Parent
        public ICollection<Branch> ChildBranches { get; set; } // Navigation property for Children
        public string? createBy { get; set; }
        public DateTime? createDate { get; set; }
        public string? updateBy { get; set; }
        public DateTime? updateDate { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Parcel>? Parcels { get; set; }
    }
}
