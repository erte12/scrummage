using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Scrummage.Models.Configuration
{
    public class ScrumTaskConfiguration : EntityTypeConfiguration<ScrumTask>
    {
        public ScrumTaskConfiguration()
        {
            Property(s => s.Content)
                .IsRequired()
                .HasMaxLength(400);

            HasOptional(s => s.User)
                .WithMany(m => m.ScrumTasks)
                .Map(cs =>
                {
                    cs.MapKey("UserId");
                });

        }
    }
}