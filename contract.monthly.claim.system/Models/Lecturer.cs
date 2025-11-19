using System.Security.Claims;

namespace contract.monthly.claim.system.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Role can be Lecturer, Coordinator, or Manager
        public string Role { get; set; } = "Lecturer";
        public ICollection<Claim>? Claims { get; set; }
    }
}
