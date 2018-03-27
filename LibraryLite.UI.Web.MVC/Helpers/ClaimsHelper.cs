using LibraryLite.UI.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace LibraryLite.UI.Web.MVC.Helpers
{
    public static class ClaimsHelper
    {
        public static MultiSelectList GetClaims()
        {
            var userClaims = UserClaim.UserClaims;
            List<SelectListItem> claimValues = new List<SelectListItem>();

            var claims = userClaims.Select(c => new SelectListItem
            {
                Text = c,
                Value = c
            }).ToList();

            MultiSelectList claimList = new MultiSelectList(claims.OrderBy(i => i.Text), "Value", "Text");
            return claimList;
        }
        public static IList<Claim> CreateClaims(List<string> claims)
        {
            IList<Claim> newUserClaims = new List<Claim>();
            if(claims!=null){
                foreach (var claim in claims)
                {
                    var userClaim = new Claim(ClaimTypes.Role,claim,ClaimValueTypes.String);
                    newUserClaims.Add(userClaim);
                }
            }
            return newUserClaims;
        }
    }
}