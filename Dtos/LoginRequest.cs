using System.ComponentModel.DataAnnotations;

namespace Architecture.Dtos;

public class LoginRequest
{
    [Required]
    [MinLength(Consts.UsernameMinLength, ErrorMessage = Consts.UsernameLengthValidationError)]
    public string? Username { get; set; }


    [Required]
    [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordValidationError)]
    public string? Password { get; set; }
}