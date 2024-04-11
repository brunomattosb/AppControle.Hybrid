using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.EntitiesDTO;
public class CountryDTO
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }

}