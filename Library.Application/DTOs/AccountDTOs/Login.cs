using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs.AccountDTOs;

public class Login
{
    [Required(ErrorMessage = "O campo {0} é obligatorio.")]
    [EmailAddress(ErrorMessage = "Você deve inserir um email válido.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Senha")]
    [Required(ErrorMessage = "O campo {0} é obligatorio.")]
    [MinLength(6, ErrorMessage = "O campo {0} deve conter ao menos {1} caracteres.")]
    public string Password { get; set; } = null!;
}