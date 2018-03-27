using LibraryLite.UI.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace LibraryLite.UI.Web.MVC.Helpers
{
    internal class AuthenticationHelper
    {
        internal static List<Claim> CreateClaim(string[] roles)
        {
            var claims = new List<Claim>();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}