using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs.AccountDTOs;
public class ChangePasswordDTO
{
    [DataType(DataType.Password)]
    [Display(Name = "Senha atual")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string CurrentPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Nova senha")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string NewPassword { get; set; } = null!;

    [Compare("NewPassword", ErrorMessage = "A senha e a confirmação não são iguais.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmação nova senha")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Confirm { get; set; } = null!;
}
