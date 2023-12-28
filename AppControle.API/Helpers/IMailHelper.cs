

using AppControle.Shared.Responses;

namespace AppControleAPI.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }

}
