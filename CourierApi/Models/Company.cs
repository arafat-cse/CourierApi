using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int companyId { get; set; }
        [Required, Display(Name = "Company Name")]
        public string? companyName { get; set; }
        public string? createBy { get; set; }
        public string? createDate { get; set; }
        public virtual ICollection<Bank>? Banks { get; set; }
    }
}
