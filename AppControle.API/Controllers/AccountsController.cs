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
    private readonly IUserHelper _userHelper;
    private readonly IConfiguration _configuration;
    //private readonly IFileStorage _fileStorage;
    private readonly IMailService _mailService;
    private readonly DataContext _context;
    private readonly string _container;
    private readonly ITokenService _tokenService;

    public AccountsController(ITokenService tokenService, IUserHelper userHelper, IConfiguration configuration, DataContext context, IMailService mailService) //IFileStorage fileStorage,
    {
        _userHelper = userHelper;
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
        var result = await _userHelper.LoginAsync(model);
        if (result.Succeeded)
        {
            var user = await _userHelper.GetUserAsync(model.Email);
            return Ok(BuildToken(user));
        }

        if (result.IsLockedOut)
        {
            return BadRequest("Número máximo de tentativas realizadas, tente novamente em 5 minutos.");
        }

        if (result.IsNotAllowed)
        {
            return BadRequest("Usuario não habilitado, siga as instruções enviadas para o email para completar o cadastro.");
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

        //await _userManager.UpdateAsync(user);
        return new TokenDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo,
            RefreshToken = refreshToken,
        };
    }
    //[HttpPost("CreateUser")]
    //public async Task<ActionResult> CreateUser([FromBody] UserDTO model)
    //{
    //    User user = model;
    //    if (!string.IsNullOrEmpty(model.Photo))
    //    {
    //        var photoUser = Convert.FromBase64String(model.Photo);
    //        model.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
    //    }

    //    var result = await _userHelper.AddUserAsync(user, model.Password);
    //    if (result.Succeeded)
    //    {
    //        await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());
    //        var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
    //        var tokenLink = Url.Action("ConfirmEmail", "accounts", new
    //        {
    //            userid = user.Id,
    //            token = myToken
    //        }, HttpContext.Request.Scheme, _configuration["UrlWEB"]);

    //        var response = _mailHelper.SendMail(user.FullName, user.Email!,
    //            $"Sales - Confirmación de cuenta",
    //            $"<h1>Sales - Confirmación de cuenta</h1>" +
    //            $"<p>Para habilitar el usuario, por favor hacer clic 'Confirmar Email':</p>" +
    //            $"<b><a href ={tokenLink}>Confirmar Email</a></b>");

    //        if (response.IsSuccess)
    //        {
    //            return NoContent();
    //        }

    //        return BadRequest(response.Message);
    //    }

    //    return BadRequest(result.Errors.FirstOrDefault());
    //}


}


