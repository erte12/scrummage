using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Scrummage.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Team> RequestedTeams { get; set; }
        public ICollection<Team> ManagedTeams { get; set; }
        public ICollection<ScrumTask> ScrumTasks { get; set; }
        
        public int? DefaultTeamId { get; set; }
        public Team DefaultTeam { get; set; }

        public ApplicationUser()
        {
            Teams = new List<Team>();
            ScrumTasks = new List<ScrumTask>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("DefaultTeamId", DefaultTeamId.ToString()));

            return userIdentity;
        }
    }
}