using Architecture.Dtos;
using Architecture.Extensions;
using Architecture.Models;
using Architecture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailSender _emailSender;

    public UserController(IAuthenticationService authenticationService, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
    {
        _authenticationService = authenticationService;
        _userManager = userManager;
        _roleManager = roleManager;
        _emailSender = emailSender;
    }

    //Fetch roles
    [HttpGet]
    [Route("GetAllRoles")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAllRoles()
    {
        var roles = _roleManager.Roles.Select(role => role.Name).ToList();
        return Ok(roles);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authenticationService.Login(request);

        var resultDto = result.ToResultDto();

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }
        return Ok(resultDto);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {

        var result = await _authenticationService.Register(request);

        var resultDto = result.ToResultDto();

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }

        string otp = GenerateRandomOTP();

        // Assign roles to the user after successful registration
        if (request.Roles != null && request.Roles.Any())
        {
            var registeredUser = await _userManager.FindByNameAsync(request.Username);
            if (registeredUser != null)
            {
                registeredUser.ResetPasswordOTP = otp;
                var updateResult = await _userManager.UpdateAsync(registeredUser);
                foreach (var roleName in request.Roles)
                {
                    if (await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _userManager.AddToRoleAsync(registeredUser, roleName);
                    }
                }

            }
        }

        var message = new Message(new string[] { request.Email }, //User email
            "Welcome to Rose Management System", // Email subject
            "Dear " + request.Username + "\r\n\r\n\r\n" +
            "" +
            "We are excited to welcome you to Little Rose! You have successfully registered for an account, and we're thrilled to have you as part of our community.\r\n\r\n\r\n" +
            "" +
            "Your account details:\r\n" + "- Username: "+ request.Username + "\r\n" + "- Email: " + request.Email + "\r\n" + "- Password: " + request.Password + "\r\n" + "- Your OTP for password reset is: " + otp + "\r\n"  + "- Registration Date: " + DateTime.Now +"\r\n\r\n" +
            "" +
            "With your Rose Management System account, you can access:\r\n" +
            "- The section/s of the system based on your role, assigned by the administrator." + "\r\n\r\n" +
            "" +
            "" +
            "If you have any questions or need assistance, please don't hesitate to reach out to our support team at info@littlerose.com.\r\n\r\n\r\n" +
            "" +
            "Thank you for choosing Little Rose. We look forward to providing you with a great experience!\r\n\r\n" +
            "" +
            "Best regards,\r\n" +
            "The Rose Management Team"); //Email message
        _emailSender.SendEmail(message);

        return Ok(resultDto);
    }

    //Register Admin
    [AllowAnonymous]
    [HttpPost("registerAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
    {

        var result = await _authenticationService.RegisterAdmin(request);

        var resultDto = result.ToResultDto();

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }

        string otp = GenerateRandomOTP();

        // Assign roles to the user after successful registration
        if (request.Roles != null && request.Roles.Any())
        {
            var registeredUser = await _userManager.FindByNameAsync(request.Username);
            if (registeredUser != null)
            {
                registeredUser.ResetPasswordOTP = otp;
                var updateResult = await _userManager.UpdateAsync(registeredUser);
                foreach (var roleName in request.Roles)
                {
                    if (await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _userManager.AddToRoleAsync(registeredUser, roleName);
                    }
                }

            }
        }

        var message = new Message(new string[] { request.Email }, //User email
            "Welcome to Rose Management System", // Email subject
            "Dear " + request.Username + "\r\n\r\n\r\n" +
            "" +
            "We are excited to welcome you to Little Rose! You have successfully registered for an account, and we're thrilled to have you as part of our community.\r\n\r\n\r\n" +
            "" +
            "Your account details:\r\n" + "- Username: " + request.Username + "\r\n" + "- Email: " + request.Email + "\r\n" + "- Password: " + request.Password + "\r\n" + "- Your OTP for password reset is: " + otp + "\r\n" + "- Registration Date: " + DateTime.Now + "\r\n\r\n" +
            "" +
            "With your Rose Management System account, you can access:\r\n" +
            "- The section/s of the system based on your role, assigned by the administrator." + "\r\n\r\n" +
            "" +
            "" +
            "If you have any questions or need assistance, please don't hesitate to reach out to our support team at info@littlerose.com.\r\n\r\n\r\n" +
            "" +
            "Thank you for choosing Little Rose. We look forward to providing you with a great experience!\r\n\r\n" +
            "" +
            "Best regards,\r\n" +
            "The Rose Management Team"); //Email message
        _emailSender.SendEmail(message);

        return Ok(resultDto);
    }


    //Password reset

    private string GenerateRandomOTP()
    {
        const string chars = "0123456789";
        var random = new Random();
        var otp = new string(Enumerable.Repeat(chars, 6) // 6 is the OTP length
            .Select(s => s[random.Next(s.Length)]).ToArray());
        return otp;
    }

    [AllowAnonymous]
    [HttpPost("reset-password-request")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
    public async Task<IActionResult> ResetPasswordRequest([FromBody] ResetPasswordRequest request)
    {
        // Generate OTP and send it to the user's email
        // Save the OTP and its expiration time in the database for verification

        // Send the OTP to the user's email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            var otp = GenerateRandomOTP(); // Implement this function to generate a random OTP
            var otpExpirationTime = DateTime.UtcNow.AddMinutes(15); // OTP valid for 15 minutes

            // Save the OTP and its expiration time in the database
            user.ResetPasswordOTP = otp;
            user.ResetPasswordOTPExpiration = otpExpirationTime;
            await _userManager.UpdateAsync(user);

            // Send the OTP to the user's email
            var emailMessage = new Message(
                new string[] { request.Email },
                "Password Reset OTP",
                $"Your OTP for password reset is: {otp}"
            );
            _emailSender.SendEmail(emailMessage);

        }
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && !string.IsNullOrEmpty(user.ResetPasswordOTP) &&
            user.ResetPasswordOTPExpiration.HasValue && user.ResetPasswordOTPExpiration > DateTime.UtcNow)
        {
            if (model.OTP == user.ResetPasswordOTP)
            {
                // Reset the user's password
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);
                if (resetResult.Succeeded)
                {
                    // Clear the OTP fields in the database
                    user.ResetPasswordOTP = null;
                    user.ResetPasswordOTPExpiration = null;
                    await _userManager.UpdateAsync(user);

                    
                }
            }
        }

        return Ok();
    }

    //Delete profile
    [HttpDelete("DeleteUser/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
    public async Task<IActionResult> DeleteUser(string username)
    {
        // Find the user by ID or other criteria
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return NotFound(); // User not found
        }

        // Perform user deletion logic here
        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            // User deleted successfully
            return new JsonResult("User deleted successfully");
        }
        else
        {
            // User deletion failed
            return BadRequest("Internal server error/ Bad request");
        }
    }

}
