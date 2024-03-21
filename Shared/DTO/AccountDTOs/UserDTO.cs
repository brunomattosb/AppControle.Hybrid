using Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public class UserDTO : User
    {
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "A senha e a confirmação não são iguais.")]
        [Display(Name = "Confirmação de senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
        public string? PasswordConfirm { get; set; }
    }
}
