using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace AppControle.API.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;
    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException is MySqlException mySqlException && mySqlException.Number == 1062)
            {
                // Código 1062 geralmente indica uma violação de índice único (duplicidade)
                context.Result = new ObjectResult(new { error = "Duplicidade de dados." })
                {
                    StatusCode = 409 // Código de status 409 para indicar conflito
                };
            }
            else
            {
                //TODO: Arrimar aqui
                //Exception em geral
                _logger.LogError(context.Exception, "Ocorreu um exceção não tratada: Status Code 500");

                context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitação: Status Code 500")
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
        else
        {
            //Exception em geral
            _logger.LogError(context.Exception, "Ocorreu um exceção não tratada: Status Code 500");

            context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitação: Status Code 500")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };            
        }

        context.ExceptionHandled = true;
    }
}
