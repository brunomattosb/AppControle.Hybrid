


using Shared.Response;

namespace AppControle.API.Services;

public interface IMailService
{
    Response SendMail(string toName, string toEmail, string subject, string body);
}


