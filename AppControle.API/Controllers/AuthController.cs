using AppControle.API.Context;
using AppControle.API.Services;
using AppControle.Shared.DTO;
using AppControle.Shared.DTO.AccountDTOs;
using AppControle.Shared.Entities;
using AppControle.Shared.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppControle.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    //private readonly IUserHelper _userHelper;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    //private readonly IFileStorage _fileStorage;
    private readonly IMailService _mailHelper;
    private readonly DataContext _context;
    private readonly string _container;

    public AuthController(ITokenService tokenService, IConfiguration configuration, DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMailService mailHelper)//  IFileStorage fileStorage, )
    {
        _mailHelper = mailHelper;
        //_userHelper = userHelper;
        _tokenService = tokenService;
        _configuration = configuration;
        //_fileStorage = fileStorage;
        //_mailHelper = mailHelper;
        _context = context;
        _container = "users";
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);


        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!)) 
        {
            var roles = await _userManager.GetRolesAsync(user);


            //TODO:Verificar claims
            var claims = new List<Claim>
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
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = _tokenService.GenerateAccessToken(claims, _configuration);
            
            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                               out int refreshTokenValidityInMinutes);

            user.RefreshTokenExpiryTime =
                            DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO model)
    {
        //TODO: Verificar isso aqui..
        // VERIFICAR ESSE CODIGO

        var userExists = await _userManager.FindByEmailAsync(model.Email!);

        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                   new Response { IsSuccess = false , Message = "Usuario já cadastrado!" });
        }
        User user = model;
        user.SecurityStamp = Guid.NewGuid().ToString();

        try
        {
            var result = await _userManager.CreateAsync(user, model.Password!);

            //if (!string.IsNullOrEmpty(model.Photo))
            //{
            //    var photoUser = Convert.FromBase64String(model.Photo);
            //    model.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
            //}
            if (result.Succeeded)
            {

                //await _userManager.AddToRoleAsync(user, user.UserType.ToString());

                var myToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var tokenLink = Url.Action("ConfirmEmail", "accounts", new
                {
                    userid = user.Id,
                    token = myToken
                }, HttpContext.Request.Scheme, _configuration["UrlWEB"]);

                var response = _mailHelper.SendMail(user.Name, user.Email!,
                    $"Confirmação de conta Automações Brasil",
                    $"<h1>Automações Brasil - Confirmação de conta</h1>" +
                    $"<p>Para habilitar o usuário, por valor clique em 'Confirmar Email':</p>" +
                    $"<b><a href ={tokenLink}>Confirmar Email</a></b>");

                if (response.IsSuccess)
                {
                    return Ok(new Response { IsSuccess = true, Message = "Usuario criado com sucesso!!" });
                }

                return BadRequest(response.Message);
            }
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException!.Message.Contains("Duplicate"))
            {
                return BadRequest("O CPF / CNPJ informado já possui cadastro.");
            }
            return BadRequest(e.InnerException.Message);
        }
        catch (Exception e)
        {
            if (e.Message.Contains("DuplicateEmail") || e.Message.Contains("DuplicateUserName"))
            {
                return BadRequest("O CPF / CNPJ informado já possui cadastro.");
            }

            return BadRequest(e.Message);
        }
        return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { IsSuccess = false, Message = "Não foi possivel completar seu cadastro, confira os dados informados!" });
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

        var user = await _userManager.FindByEmailAsync(email!);

        if (user == null || user.RefreshToken != refreshToken
                         || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Token de acesso inválido");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(
                                           principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpPost]
    [Route("revoke/{email}")]
    public async Task<IActionResult> Revoke(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) 
            return BadRequest("Email inválido.");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }

    //[HttpPost("CreateUser")]
    //public async Task<ActionResult> CreateUser([FromBody] UserDTO model)
    //{
    //    try
    //    {

    //        var result = await _userHelper.AddUserAsync(user, model.Password);

    //        

    //        throw new Exception(result.Errors?.FirstOrDefault()?.Code);

    //    }
    //    











    //[HttpPost("RecoverPassword")]
    //public async Task<ActionResult> RecoverPassword([FromBody] EmailDTO model)
    //{
    //    User user = await _userHelper.GetUserAsync(model.Email);
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
    //    var tokenLink = Url.Action("ResetPassword", "accounts", new
    //    {
    //        userid = user.Id,
    //        token = myToken
    //    }, HttpContext.Request.Scheme, _configuration["UrlWEB"]);

    //    var response = _mailHelper.SendMail(user.Name, user.Email!,
    //        $"Sales - Recuperação de senha",
    //        $"<h1>Sales - Recuperação de senha</h1>" +
    //        $"<p>Para recuperar sua senha, por favor clicar em 'Recuperar Senha':</p>" +
    //        $"<b><a href ={tokenLink}>Recuperar Senha</a></b>");

    //    if (response.IsSuccess)
    //    {
    //        return NoContent();
    //    }

    //    return BadRequest(response.Message);
    //}

    //[HttpPost("ResetPassword")]
    //public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
    //{
    //    User user = await _userHelper.GetUserAsync(model.Email);
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
    //    if (result.Succeeded)
    //    {
    //        return NoContent();
    //    }

    //    return BadRequest(result.Errors.FirstOrDefault()!.Description);
    //}
    //[HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //public async Task<ActionResult> Get()
    //{
    //    var teste = await _userHelper.GetUserAsync(User.FindFirstValue(ClaimTypes.Sid));
    //    return Ok(await _userHelper.GetUserAsync(User.FindFirstValue(ClaimTypes.Email)));
    //}


    //[HttpPost("changePassword")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //public async Task<ActionResult> ChangePasswordAsync(ChangePasswordDTO model)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    var user = await _userHelper.GetUserAsync(User.FindFirstValue(ClaimTypes.Email));
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    var result = await _userHelper.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
    //    if (!result.Succeeded)
    //    {
    //        return BadRequest(result.Errors.FirstOrDefault()!.Description);
    //    }

    //    return NoContent();
    //}
    //[HttpPut]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //public async Task<ActionResult> Put(User user)
    //{
    //    try

    //    {
    //        if (!string.IsNullOrEmpty(user.Photo))
    //        {
    //            var photoUser = Convert.FromBase64String(user.Photo);
    //            user.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
    //        }

    //        var currentUser = await _userHelper.GetUserAsync(user.Email!);
    //        if (currentUser == null)
    //        {
    //            return NotFound();
    //        }

    //        currentUser.Address = user.Address;
    //        currentUser.AddressNumber = user.AddressNumber;
    //        currentUser.AddressCep = user.AddressCep;
    //        currentUser.Cpf_Cnpj = user.Cpf_Cnpj;
    //        currentUser.FantasyName = user.FantasyName;
    //        currentUser.IM = user.IM;
    //        currentUser.Name = user.Name;
    //        currentUser.Neighborhood = user.Neighborhood;
    //        currentUser.Rg_Ie = user.Rg_Ie;
    //        currentUser.UserName = user.UserName;
    //        currentUser.PhoneNumber = user.PhoneNumber;
    //        currentUser.Photo = !string.IsNullOrEmpty(user.Photo) && user.Photo != currentUser.Photo ? user.Photo : currentUser.Photo;
    //        currentUser.CityId = user.CityId;

    //        var result = await _userHelper.UpdateUserAsync(currentUser);
    //        if (result.Succeeded)
    //        {
    //            return Ok(BuildToken(currentUser));
    //        }

    //        return BadRequest(result.Errors.FirstOrDefault());
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
    //private TokenDTO BuildToken(User user)
    //{
    //    var claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Email, user.Email!),
    //        new Claim(ClaimTypes.Role, user.UserType.ToString()),
    //        new Claim("Document", user.Cpf_Cnpj!),
    //        new Claim(ClaimTypes.Name, user.Name!),
    //        new Claim(ClaimTypes.Sid, user.Id),
    //        new Claim("Address", user.Address!),
    //        new Claim("Photo", user.Photo ?? string.Empty),
    //        new Claim("CityId", user.CityId.ToString())
    //    };

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]!));
    //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    //    //expirationtoken
    //    var expiration = DateTime.UtcNow.AddDays(30);
    //    var token = new JwtSecurityToken(
    //        issuer: null,
    //        audience: null,
    //        claims: claims,
    //        expires: expiration,
    //        signingCredentials: credentials);

    //    return new TokenDTO
    //    {
    //        Token = new JwtSecurityTokenHandler().WriteToken(token),
    //        Expiration = expiration
    //    };
    //}
    //[HttpGet("ConfirmEmail")]
    //public async Task<ActionResult> ConfirmEmailAsync(string userId, string token)
    //{
    //    token = token.Replace(" ", "+");
    //    var user = await _userHelper.GetUserAsync(new Guid(userId));
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    var result = await _userHelper.ConfirmEmailAsync(user, token);
    //    if (!result.Succeeded)
    //    {
    //        return BadRequest(result.Errors.FirstOrDefault());
    //    }

    //    return NoContent();
    //}
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



    //}
    //[HttpGet("all")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //public async Task<ActionResult> GetAll([FromQuery] Pagination pagination, [FromQuery] string? filter)
    //{
    //    var queryable = _context.Users
    //        .Include(u => u.City)
    //        .AsQueryable();

    //    if (!string.IsNullOrWhiteSpace(filter))
    //    {
    //        queryable = queryable.Where(x => x.Name!.ToLower().Contains(filter.ToLower()));
    //    }
    //    await HttpContext.InsertParamsInPageResponse(queryable, pagination.QuantityPerPage);
    //    return Ok(await queryable
    //        .OrderBy(x => x.Name)
    //        //.OrderBy(x => x.FirstName)
    //        //.ThenBy(x => x.LastName)
    //        .Paginar(pagination)
    //        .ToListAsync());
    //}

}