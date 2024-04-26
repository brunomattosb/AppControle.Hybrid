using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs.EntitiesDTO;
public class CategoryDTO
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }

}