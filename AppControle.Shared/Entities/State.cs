using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControle.Shared.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo {0} não pode termais de {1} caractéres")]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }


        public Country? Country { get; set; }

        public ICollection<City>? Cities { get; set; }

    }
}
