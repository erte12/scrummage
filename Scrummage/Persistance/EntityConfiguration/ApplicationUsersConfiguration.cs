using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Scrummage.Models.Configuration
{
    public class ApplicationUsersConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUsersConfiguration()
        {
            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(60);

            Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}