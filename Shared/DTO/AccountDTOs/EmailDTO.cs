using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.AccountDTOs;

public class EmailDTO
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "O campo {0} é obligatorio.")]
    [EmailAddress(ErrorMessage = "Você deve inserir um email válido.")]
    public string Email { get; set; } = null!;
}
