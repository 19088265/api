using Microsoft.AspNetCore.Identity;
namespace Architecture.Models
{
    public class AppUser : IdentityUser
    {
        // New properties for OTP
        public string? ResetPasswordOTP { get; set; }
        public DateTime? ResetPasswordOTPExpiration { get; set; }
    }
}
