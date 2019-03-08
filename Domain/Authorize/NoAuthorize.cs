using Microsoft.AspNetCore.Mvc;

namespace Domain.Authorize
{
    public static class NoAuthorize
    {
        public static object DenyAccess()
        {
            var naoAutorizado = new ObjectResult(new { statusCode = 401, message = "Não autorizado!" })
            {
                StatusCode = 401
            };

            return naoAutorizado;
        }

        public static object LogOut()
        {
            var logOut = new ObjectResult(new { statusCode = 401, message = "Sessão inválida" })
            {
                StatusCode = 401
            };

            return logOut;
        }
    }
}
