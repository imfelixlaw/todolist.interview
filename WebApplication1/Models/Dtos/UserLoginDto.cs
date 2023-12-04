using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Dtos;

public class UserLoginDto
{
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
}
