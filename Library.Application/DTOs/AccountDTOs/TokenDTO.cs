
namespace Library.Application.DTOs.AccountDTOs;
public class TokenDTO
{
    public string? Token { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}