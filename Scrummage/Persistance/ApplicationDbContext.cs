using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Scrummage.Core.Domain;
using Scrummage.Models.Configuration;
using Scrummage.Persistance.EntityConfiguration;

namespace Scrummage.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<ScrumTask> ScrumTasks { get; set; }
        public DbSet<Estimation> Estimations { get; set; }
        public DbSet<Event> Events { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                .Add(new ApplicationUsersConfiguration())
                .Add(new SprintConfiguration())
                .Add(new ScrumTaskConfiguration())
                .Add(new TeamsConfiguration())
                .Add(new EventConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}