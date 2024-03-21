using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Entities;
public class City
{
    public int Id { get; set; }

    [Display(Name = "Cidade")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo {0} não pode ter mais de {1} caracteres")]
    public string Name { get; set; } = null!;

    [Display(Name = "Código da Cidade")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(10, ErrorMessage = "O campo {0} não pode ter mais de {1} caracteres")]
    public int CodCity { get; set; }

    public int StateId { get; set; }

    public State? State { get; set; }

    [JsonIgnore]
    public ICollection<User>? lUsers { get; set; }
}
