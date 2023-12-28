using Microsoft.EntityFrameworkCore;

namespace AppControle.API.Extensions
{
    public static class HttpContextExtensions
    {

        public async static Task InsertParamsInPageResponse<T>(this HttpContext context,
           IQueryable<T> queryable, int totalNumberOfRecordsToDisplay)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double totalRecordsQuantity = await queryable.CountAsync();
            double totalPages = Math.Ceiling(totalRecordsQuantity / totalNumberOfRecordsToDisplay);

            //salvando as informações no header do response
            context.Response.Headers.Add("totalRecordsQuantityHeaders", totalRecordsQuantity.ToString());
            context.Response.Headers.Add("totalPagesHeaders", totalPages.ToString());
        }
    }
}
