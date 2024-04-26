using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs.EntitiesDTO;
public class CountryDTO
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }

}