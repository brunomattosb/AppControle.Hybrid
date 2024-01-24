using System.ComponentModel.DataAnnotations;

namespace AppControle.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obligatorio.")]
        public string Name { get; set; } = null!;

        public ICollection<ProductCategory>? ProductCategories { get; set; }

        [Display(Name = "Produtos")]
        public int ProductCategoriesNumber => ProductCategories == null ? 0 : ProductCategories.Count;
    }
}
