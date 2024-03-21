using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Shared.DTO.EntitiesDTO;

namespace Shared.Entities;
public class Category : UserUnionDTO
{
    public Category()
    {
        lProductCategories = new Collection<ProductCategory>();
    }

    [Key]
    public int Id { get; set; }

    [Display(Name = "Categoria")]
    [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obligatorio.")]
    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<ProductCategory>? lProductCategories { get; set; }
}
