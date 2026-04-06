using System.Net;
using System.Text.Json;

namespace Gerenciamento_PetShop.Presentation.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Deixa a requisição seguir para o Controller normalmente
                await _next(context);
            }
            catch (Exception ex)
            {
                // Se o Controller "estourar" um erro, ele cai aqui em vez de quebrar a API
                _logger.LogError(ex, "Ocorreu um erro não tratado na aplicação.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Status 500

            // Monta um JSON amigável para quem estiver consumindo a API
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro interno no servidor. Nossa equipe técnica já foi notificada.",
                Detalhe = exception.Message // Mostra o erro real (útil para debug)
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}