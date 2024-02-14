using AppControle.Shared.Entities.Pagination.Pagination;

namespace AppControle.Shared.Entities.Pagination;

public class FiltersProduct : QueryStringParameters
{
    public string? PriceCriterion { get; set; } //maior, menor, igual
    public decimal? Price { get; set; }
}

