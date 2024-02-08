using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppControle.Shared.Entities;
public class Category : UserUnion
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
