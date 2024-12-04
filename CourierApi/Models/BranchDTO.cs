namespace CourierApi.Models
{
    public class BranchDTO
    {
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? Address { get; set; }
        public int? ParentId { get; set; } // শুধু Parent-এর ID পাঠাবেন
        public List<BranchDTO>? ChildBranches { get; set; } // Recursive structure (সাব-শাখার জন্য)
        public bool IsActive { get; set; }
    }
}
