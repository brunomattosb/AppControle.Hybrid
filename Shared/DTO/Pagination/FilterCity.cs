using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Pagination;

public class FilterCity : QueryStringParameters
{
    public int id { get; set; }
}