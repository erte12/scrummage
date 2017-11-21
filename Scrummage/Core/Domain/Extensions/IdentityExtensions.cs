using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Scrummage.Core.Domain.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetDefaultTeamId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("DefaultTeamId");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}