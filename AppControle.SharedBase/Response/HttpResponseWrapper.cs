//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace AppControle.Shared.Response
//{
//    public class HttpResponseWrapper<T>
//    {
//        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
//        {
//            Error = error;
//            Response = response;
//            HttpResponseMessage = httpResponseMessage;
//        }
//        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage, int totalPages)
//        {
//            Error = error;
//            Response = response;
//            HttpResponseMessage = httpResponseMessage;
//            TotalPages = totalPages;
//        }

//        public bool Error { get; set; } = true;
//        public int TotalPages { get; set; }

//        public T? Response { get; set; }

//        public HttpResponseMessage HttpResponseMessage { get; set; }

//        public async Task<string?> GetErrorMessageAsync()
//        {
//            if (!Error)
//            {
//                return null;
//            }

//            var statusCode = HttpResponseMessage.StatusCode;
//            if (statusCode == HttpStatusCode.NotFound)
//            {
//                return "Recurso não encontrado";
//            }
//            else if (statusCode == HttpStatusCode.BadRequest)
//            {
//                return await HttpResponseMessage.Content.ReadAsStringAsync();
//            }
//            else if (statusCode == HttpStatusCode.Unauthorized)
//            {
//                return "Você precisa se logar para fazer esta operação";
//            }
//            else if (statusCode == HttpStatusCode.Forbidden)
//            {
//                return "Não há permissão para fazer esta operação";
//            }

//            return "Ocorreu um erro inesperado ";
//        }
//    }
//}
