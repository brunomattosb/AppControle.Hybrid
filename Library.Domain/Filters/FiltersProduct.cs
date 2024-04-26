namespace Library.Domain.Filters;

public class FiltersProduct : QueryStringParameters
{
    public string? PriceCriterion { get; set; } //maior, menor, igual
    public decimal? Price { get; set; }
}

