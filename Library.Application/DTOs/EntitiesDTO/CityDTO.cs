using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs.EntitiesDTO;
public class CityDTO
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }


}