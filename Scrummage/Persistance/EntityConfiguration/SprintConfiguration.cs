using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Scrummage.Models.Configuration
{
    public class SprintConfiguration : EntityTypeConfiguration<Sprint>
    {
        public SprintConfiguration()
        {
            Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(60);

            Property(s => s.Description)
                .HasMaxLength(1000);
        }
    }
}