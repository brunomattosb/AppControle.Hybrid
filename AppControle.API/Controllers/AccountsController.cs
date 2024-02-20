using AppControle.API.Context;
using AppControle.API.Repositories;
using AppControle.API.Services;
using AppControle.Shared.DTO;
using AppControle.Shared.DTO.AccountDTOs;
using AppControle.Shared.Entities;
using AppControle.Shared.Enums;
using AppControle.Shared.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;

namespace AppControle.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    //private readonly IFileStorage _fileStorage;
    private readonly IMailService _mailService;
    private readonly DataContext _context;
    private readonly string _container;
    private readonly ITokenService _tokenService;

    public AccountsController(ITokenService tokenService, IUserRepository userRepository, IConfiguration configuration, DataContext context, IMailService mailService) //IFileStorage fileStorage,
    {
        _userRepository = userRepository;
        _configuration = configuration;
        //_fileStorage = fileStorage;
        _mailService = mailService;
        _context = context;
        _tokenService = tokenService;
        _container = "users";
    }
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO model)
    {
        var result = await _userRepository.LoginAsync(model);
        if (result.Succeeded)
        {
            var user = await _userRepository.GetUserAsync(model.Email);
            var token = BuildToken(user);
            user.RefreshToken = token.RefreshToken;
            await _userRepository.UpdateUserAsync(user);

            return Ok(token);
        }

        if (result.IsLockedOut)
        {
            return StatusCode(StatusCodes.Status400BadRequest,
                       new Response { IsSuccess = false, Message = "Número máximo de tentativas realizadas, tente novamente em 5 minutos." });
        }

        if (result.IsNotAllowed)
        {
            return StatusCode(StatusCodes.Status400BadRequest,
                       new Response { IsSuccess = false, Message = "Usuario não habilitado, siga as instruções enviadas para o email para completar o cadastro." });
        }

        return BadRequest("Email ou senha incorrectos.");
    }
    private TokenDTO BuildToken(User user)
    {
        var lclaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                //new Claim("Document", user.Cpf_Cnpj!),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Sid, user.Id),
                //new Claim("Address", user.Address!),
                new Claim("Photo", user.Photo ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim("CityId", user.CityId.ToString())
        };

        var token = _tokenService.GenerateAccessToken(lclaims, _configuration);
        var refreshToken = _tokenService.GenerateRefreshToken();
        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                               out int refreshTokenValidityInMinutes);
        user.RefreshTokenExpiryTime =
                            DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);
        user.RefreshToken = refreshToken;

        

        return new TokenDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo,
            RefreshToken = refreshToken,
        };
    }

    [HttpPost("CreateUser")]
    public async Task<ActionResult> CreateUser([FromBody] UserDTO userDTO)
    {
        
        User user = userDTO;
        //if (!string.IsNullOrEmpty(userDTO.Photo))
        //{
        //    var photoUser = Convert.FromBase64String(userDTO.Photo);
        //    userDTO.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
        //}
        user.UserName = user.Email;
        var result = await _userRepository.AddUserAsync(user, userDTO.Password!);
        if (result.Succeeded)
        {
            await _userRepository.AddUserToRoleAsync(user, user.UserType.ToString());
            //return Ok(BuildToken(user));

            var myToken = await _userRepository.GenerateEmailConfirmationTokenAsync(user);
            var tokenLink = Url.Action("ConfirmEmail", "accounts", new
            {
                userid = user.Id,
                token = myToken
            }, HttpContext.Request.Scheme, _configuration["UrlWEB"]);

            var response = _mailService.SendMail(user.Name!, user.Email!,
                $"Confirmação de conta Automações Brasil",
                $"<h1>Automações Brasil - Confirmação de conta</h1>" +
                $"<p>Para habilitar o usuário, por valor clique em 'Confirmar Email':</p>" +
                $"<b><a href ={tokenLink}>Confirmar Email</a></b>");

            //if (response.IsSuccess)
            //{
                return Ok(new Response { IsSuccess = true, Message = "Usuario criado com sucesso!" });
            //}
        }
        else
        {
            throw new DbUpdateException(result.Errors?.FirstOrDefault()!.Code!);
        }
    }

    [HttpPost("revoke/{email}")]
    public async Task<IActionResult> RevokeToken(string email)
    {
        var user = await _userRepository.GetUserAsync(email);

        if (user == null)
            return BadRequest(new Response { IsSuccess = true, Message = "Email inválido" });

        user.RefreshToken = null;

        await _userRepository.UpdateUserAsync(user);

        return NoContent();
    }

    [HttpGet("ConfirmEmail")]
    public async Task<ActionResult> ConfirmEmailAsync(string userId, string token)
    {
        token = token.Replace(" ", "+");
        var user = await _userRepository.GetUserAsync(new Guid(userId));
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userRepository.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.FirstOrDefault());
        }

        return NoContent();
    }
    [HttpPost("RecoverPassword")]
    public async Task<ActionResult> RecoverPassword([FromBody] EmailDTO model)
    {
        User user = await _userRepository.GetUserAsync(model.Email);
        if (user == null)
        {
            return NotFound();
        }

        var myToken = await _userRepository.GeneratePasswordResetTokenAsync(user);
        var tokenLink = Url.Action("ResetPassword", "accounts", new
        {
            userid = user.Id,
            token = myToken
        }, HttpContext.Request.Scheme, _configuration["UrlWEB"]);

        var response = _mailService.SendMail(user.Name, user.Email!,
            $"Sales - Recuperação de senha",
            $"<h1>Sales - Recuperação de senha</h1>" +
            $"<p>Para recuperar sua senha, por favor clicar em 'Recuperar Senha':</p>" +
            $"<b><a href ={tokenLink}>Recuperar Senha</a></b>");

        if (response.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(response.Message);
    }
    [HttpPost("ResetPassword")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
    {
        User user = await _userRepository.GetUserAsync(model.Email);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userRepository.ResetPasswordAsync(user, model.Token, model.Password);
        if (result.Succeeded)
        {
            return NoContent();
        }

        return BadRequest(result.Errors.FirstOrDefault()!.Description);
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Get()
    {
        var teste = await _userRepository.GetUserAsync(User.FindFirstValue(ClaimTypes.Sid));
        return Ok(await _userRepository.GetUserAsync(User.FindFirstValue(ClaimTypes.Email)));
    }
    [HttpPost("changePassword")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> ChangePasswordAsync(ChangePasswordDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userRepository.GetUserAsync(User.FindFirstValue(ClaimTypes.Email));
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userRepository.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.FirstOrDefault()!.Description);
        }

        return NoContent();
    }
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Put(User user)
    {
        try

        {
            //if (!string.IsNullOrEmpty(user.Photo))
            //{
            //    var photoUser = Convert.FromBase64String(user.Photo);
            //    user.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
            //}

            var currentUser = await _userRepository.GetUserAsync(user.Email!);
            if (currentUser == null)
            {
                return NotFound();
            }

            currentUser.Address = user.Address;
            currentUser.AddressNumber = user.AddressNumber;
            currentUser.AddressCep = user.AddressCep;
            currentUser.Cpf_Cnpj = user.Cpf_Cnpj;
            currentUser.FantasyName = user.FantasyName;
            currentUser.IM = user.IM;
            currentUser.Name = user.Name;
            currentUser.Neighborhood = user.Neighborhood;
            currentUser.Rg_Ie = user.Rg_Ie;
            currentUser.UserName = user.UserName;
            currentUser.PhoneNumber = user.PhoneNumber;
            currentUser.Photo = !string.IsNullOrEmpty(user.Photo) && user.Photo != currentUser.Photo ? user.Photo : currentUser.Photo;
            currentUser.CityId = user.CityId;

            var result = await _userRepository.UpdateUserAsync(currentUser);
            if (result.Succeeded)
            {
                return Ok(BuildToken(currentUser));
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenDTO tokenDTO)
    {

        if (tokenDTO is null)
        {
            return BadRequest("Requisição inválida");
        }

        string? accessToken = tokenDTO.Token
                              ?? throw new ArgumentNullException(nameof(tokenDTO));

        string? refreshToken = tokenDTO.RefreshToken
                               ?? throw new ArgumentException(nameof(tokenDTO));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

        if (principal == null)
        {
            return BadRequest("Token de acesso inválido");
        }

        string email = principal.FindFirstValue(ClaimTypes.Email)!;

        var user = await _userRepository.GetUserAsync(email!);

        if (user == null || user.RefreshToken != refreshToken
                         || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Token de acesso inválido");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(
                                           principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _userRepository.UpdateUserAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

}



//[HttpPost("ResedToken")]
//public async Task<ActionResult> ResedToken([FromBody] EmailDTO model)
//{
//    User user = await _userHelper.GetUserAsync(model.Email);
//    if (user == null)
//    {
//        return NotFound();
//    }

//    //TODO: Improve code
//    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
//    var tokenLink = Url.Action("ConfirmEmail", "accounts", new
//    {
//        userid = user.Id,
//        token = myToken
//    }, HttpContext.Request.Scheme, _configuration["UrlWEB"]);

//    var response = _mailHelper.SendMail(user.Name, user.Email!,
//        "Confirmação de conta Automações Brasil",
//        $"<h1>Automações Brasil - Confirmação de conta</h1>" +
//        $"<p>Para habilitar o usuário, por valor clique em 'Confirmar Email':</p>" +
//        $"<b><a href ={tokenLink}>Confirmar Email</a></b>");

//    if (response.IsSuccess)
//    {
//        return NoContent();
//    }

//    return BadRequest(response.Message);
//}
