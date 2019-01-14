using System;
using Microsoft.AspNetCore.Mvc;

namespace Superdigital.Backend.ContaCorrente.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LoginAttribute : TypeFilterAttribute
    {
        public LoginAttribute(string domain = null) : base(typeof(LoginFilter))
        {
            Arguments = new object[]
            {
                domain
            };
        }
    }
}
