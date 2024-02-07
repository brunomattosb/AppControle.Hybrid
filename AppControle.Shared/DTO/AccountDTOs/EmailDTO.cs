using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControle.Shared.DTO.AccountDTOs
{
    public class EmailDTO
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo {0} é obligatorio.")]
        [EmailAddress(ErrorMessage = "Você deve inserir um email válido.")]
        public string Email { get; set; } = null!;
    }
}
