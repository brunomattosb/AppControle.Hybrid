using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControle.Shared.DTO.AccountDTOs;
public class TokenDTO
{
    public string? Token { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}