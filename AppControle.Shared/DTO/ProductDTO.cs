using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControle.Shared.DTO;
public class ProductDTO
{
    public int Id { get; set; }

    [Display(Name = "Nome")]
    [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obligatorio.")]
    public string Name { get; set; } = null!;

    [DataType(DataType.MultilineText)]
    [Display(Name = "Descrição")]
    [MaxLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public string Description { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [Display(Name = "Preço")]
    [Required(ErrorMessage = "O campo {0} é obligatorio.")]
    public decimal Price { get; set; }

    [DisplayFormat(DataFormatString = "{0:N2}")]
    [Display(Name = "Estoque")]
    [Required(ErrorMessage = "O campo {0} é obligatorio.")]
    public float Stock { get; set; }

    public List<int>? ProductCategoryIds { get; set; }

    public bool IsActive { get; set; } = true;
    
    public bool IsService { get; set; }
    
    //public List<string>? ProductImages { get; set; }
}