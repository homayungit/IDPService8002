using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = default!;
        public string EmpCode { get; set; } = default!;
        public string? CreatedBy { get; set; } 
        public DateTime? CreatedTime { get; set; } 
        public string? UpdatedBy { get; set; } 
        public DateTime? UpdatedTime { get; set; }

    }
}
