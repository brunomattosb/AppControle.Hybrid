using AppControle.API.Data;
using AppControle.API.Helpers;
using AppControle.API.Services;
using AppControle.Shared.Entities;
using AppControleAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using SisVendas.API.Data;
using System.Text;
using System.Text.Json.Serialization;




//Mail
builder.Services.AddScoped<IMailHelper, MailHelper>();
//Azure
builder.Services.AddScoped<IFileStorage, FileStorage>();

//Seeds
builder.Services.AddTransient<SeedDb>();
//API
builder.Services.AddScoped<IApiService, ApiService>();
//Orders
//builder.Services.AddScoped<IOrdersHelper, OrdersHelper>();
//Auth
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
    x.SignIn.RequireConfirmedEmail = true;
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); //TODO: Change to 5 minutes
    x.Lockout.MaxFailedAccessAttempts = 6;
    x.Lockout.AllowedForNewUsers = true;
    x.Password.RequiredLength = 6;

}).AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero
    });





var app = builder.Build();


