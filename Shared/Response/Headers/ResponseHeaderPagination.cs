using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Response.Headers
{
    public class ResponseHeaderPagination
    {
       
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

    }
}
