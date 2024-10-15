using Auth.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Auth.Filter
{
    public class AuthorizationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
    {
        private readonly TokenService _tokenService;

        public AuthorizationFilter(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "uid")?.Value;
            var accessToken = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (accessToken != null && !string.IsNullOrEmpty(userId))
            {
                var isExists = await _tokenService.IsExistSession(accessToken, long.Parse(userId));

                if (!isExists)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
