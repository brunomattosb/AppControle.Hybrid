﻿
using Library.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library.Domain.Entities;
public class User : IdentityUser
{
    [Key]
    [Display(Name = "CPF / CNPJ")]
    [Required(ErrorMessage = "O campo {0} é obligatório.")]
    [MaxLength(14, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    //[ValidateCpf]
    //[RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. O formato deve ser XXX.XXX.XXX-XX.")]
    public string? Cpf_Cnpj { get; set; }

    [Display(Name = "Nome / Razão Social")]
    [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obligatório.")]
    public string? Name { get; set; }

    [Display(Name = "Nome Fantasia")]
    [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public string? FantasyName { get; set; }

    [Display(Name = "Foto")]
    public string? Photo { get; set; }

    [Display(Name = "Tipo de usuario")]
    public UserType UserType { get; set; }

    public City? City { get; set; }

    [Display(Name = "Cidade")]
    [Range(1, int.MaxValue, ErrorMessage = "Você deve selecionar um {0}.")]
    public int CityId { get; set; }

    [Display(Name = "RG / IE")]
    [MaxLength(19, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public string? Rg_Ie { get; set; }

    [Display(Name = "Inscrição Municipal")]
    [MaxLength(11, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public string? IM { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
    [Display(Name = "Data de Registro")]
    public DateTime RegisterDate { get; set; }

    [Display(Name = "CEP")]
    [MaxLength(8, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public string? AddressCep { get; set; }

    [Display(Name = "Logradouro")]
    [MaxLength(30, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public string? Address { get; set; }

    [Display(Name = "Número")]
    [Range(1, int.MaxValue)]
    public int AddressNumber { get; set; } = 1;

    [Display(Name = "Bairro")]
    [MaxLength(30, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public string? Neighborhood { get; set; }

    //Token
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    //Collections 
    [JsonIgnore]
    public ICollection<Product>? lProducts { get; set; }

    [JsonIgnore]
    public ICollection<Category>? Category { get; set; }
    //public ICollection<Client>? lClients { get; set; }
    //public ICollection<MonthlyFee>? lMonthlyFees { get; set; }


}