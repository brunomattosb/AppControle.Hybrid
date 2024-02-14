using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControle.Shared.DTO.EntitiesDTO;
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