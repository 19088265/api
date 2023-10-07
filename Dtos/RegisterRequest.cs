using System.ComponentModel.DataAnnotations;

namespace Architecture.Dtos;

public class RegisterRequest
{
    [Required]
    [MinLength(Consts.UsernameMinLength, ErrorMessage = Consts.UsernameLengthValidationError)]
    public string? Username { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = Consts.EmailValidationError)]
    public string? Email { get; set; }

    [Required]
    [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordValidationError)]
    public string? Password { get; set; }

    public List<string> Roles { get; set; }
}