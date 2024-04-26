namespace Library.Application.DTOs.EntitiesDTO;
public class ProductDTOList
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public float Stock { get; set; } = 0;
}