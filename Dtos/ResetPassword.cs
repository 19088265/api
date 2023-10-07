namespace Architecture.Dtos
{
    public class ResetPassword
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
    }
}
