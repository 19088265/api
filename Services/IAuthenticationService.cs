using System.IdentityModel.Tokens.Jwt;
using Architecture.Dtos;
using FluentResults;

namespace Architecture.Services;

public interface IAuthenticationService
{
    Task<Result<string>> Register(RegisterRequest request);
    Task<Result<string>> RegisterAdmin(RegisterRequest request);
    Task<Result<string>> Login(LoginRequest request);
}