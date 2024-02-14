using AppControle.Shared.Entities.Pagination.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControle.Shared.Entities.Pagination;

public class FiltersCategory : QueryStringParameters
{
    public string? Name { get; set; } 
}