using HealthcareSolutions.API.Authentication;
using HealthcareSolutions.API.Data;
using HealthcareSolutions.API.DTOs;
using HealthcareSolutions.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSolutions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JwtTokenService _jwtTokenService;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthController(
        ApplicationDbContext context,
        JwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = new PasswordHasher<User>();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(
        LoginRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username) ||
            string.IsNullOrWhiteSpace(dto.Password))
        {
            return BadRequest("Username and password are required.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u =>
                u.Username == dto.Username);

        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        if (!user.IsActive)
        {
            return Unauthorized("This user account is inactive.");
        }

        var passwordResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            dto.Password);

        if (passwordResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Invalid username or password.");
        }

        var token = _jwtTokenService.CreateToken(user);

        return Ok(new LoginResponseDto
        {
            Token = token,
            UserId = user.UserId,
            Username = user.Username,
            Role = user.Role.ToString()
        });
     }
}