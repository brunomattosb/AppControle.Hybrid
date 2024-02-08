//using System.ComponentModel.DataAnnotations;

//namespace AppControle.Shared.DTO.AccountDTOs
//{
//    public class ResetPasswordDTO
//    {
//        [Display(Name = "Email")]
//        [EmailAddress(ErrorMessage = "Deve inserir um email válido")]
//        [Required(ErrorMessage = "O campo {0} é obligatorio.")]
//        public string Email { get; set; } = null!;

//        [DataType(DataType.Password)]
//        [Display(Name = "Senha")]
//        [Required(ErrorMessage = "O campo {0} é obligatorio.")]
//        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} carácteres.")]
//        public string Password { get; set; } = null!;

//        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não são iguais.")]
//        [DataType(DataType.Password)]
//        [Display(Name = "Confirmação de senha")]
//        [Required(ErrorMessage = "O campo {0} é obligatorio.")]
//        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} carácteres.")]
//        public string ConfirmPassword { get; set; } = null!;

//        public string Token { get; set; } = null!;
//    }
//}
