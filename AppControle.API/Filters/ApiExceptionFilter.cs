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
        if (context.Exception.Message.Contains("Duplicate") ||
            context.Exception.Message.Contains("DuplicateEmail") ||
            context.Exception.Message.Contains("DuplicateUserName"))
        {
            // Código 1062 geralmente indica uma violação de índice único (duplicidade)
            context.Result = new ObjectResult(new { error = "Duplicidade de dados." })
            {
                StatusCode = 409 // Código de status 409 para indicar conflito
            };            
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
