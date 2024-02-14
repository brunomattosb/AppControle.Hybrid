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

//API
builder.Services.AddScoped<IApiService, ApiService>();
//Orders
//builder.Services.AddScoped<IOrdersHelper, OrdersHelper>();
//Auth
builder.Services.AddScoped<IUserHelper, UserHelper>();






var app = builder.Build();


