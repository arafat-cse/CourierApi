namespace CourierApi.Models
{
    public class Designation
    {
        public int designationId {  get; set; }
        public string designationName { get; set; }
        public virtual ICollection<Staff>? Staffs { get; set; }
    }
}
