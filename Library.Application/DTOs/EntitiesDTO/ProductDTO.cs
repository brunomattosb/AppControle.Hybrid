namespace Library.Application.DTOs.EntitiesDTO;
public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public float Stock { get; set; }

    public List<int>? ProductCategoryIds { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsService { get; set; }
}