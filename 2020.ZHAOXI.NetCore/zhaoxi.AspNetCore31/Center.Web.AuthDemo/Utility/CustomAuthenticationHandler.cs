using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Center.Web.AuthDemo.Utility
{
    public class CustomAuthenticationHandler : IAuthenticationHandler, IAuthenticationSignInHandler, IAuthenticationSignOutHandler
    {
        public AuthenticationScheme Scheme { get; private set; }
        protected HttpContext Context { get; private set; }

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            Scheme = scheme;
            Context = context;
            return Task.CompletedTask;
        }

        public async Task<AuthenticateResult> AuthenticateAsync()
        {
            var cookie = Context.Request.Cookies["CustomCookie.ZX"];
            if (string.IsNullOrEmpty(cookie))
            {
                return  AuthenticateResult.NoResult();
            }
            return AuthenticateResult.Success(Deserialize(cookie));
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            Context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }

        public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            var ticket = new AuthenticationTicket(user, properties, Scheme.Name);
            Context.Response.Cookies.Append("CustomCookie.ZX", Serialize(ticket));
            return Task.CompletedTask;
        }

        public Task SignOutAsync(AuthenticationProperties properties)
        {
            Context.Response.Cookies.Delete("CustomCookie.ZX");
            return Task.CompletedTask;
        }

        #region proivate method
        private AuthenticationTicket Deserialize(string content)
        {
            byte[] byteTicket = System.Text.Encoding.Default.GetBytes(content);
            return TicketSerializer.Default.Deserialize(byteTicket);
        }
        private string Serialize(AuthenticationTicket ticket)
        {
            //需要引入  Microsoft.AspNetCore.Authentication
            byte[] byteTicket = TicketSerializer.Default.Serialize(ticket);
            return Encoding.Default.GetString(byteTicket);
        }
        #endregion
    }
}
