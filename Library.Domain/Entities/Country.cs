using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library.Domain.Entities;
public class Country
{
    public int Id { get; set; }

    [Display(Name = "País")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo {0} não pode termais de {1} caracteres")]
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public ICollection<State>? lStates { get; set; }

}
