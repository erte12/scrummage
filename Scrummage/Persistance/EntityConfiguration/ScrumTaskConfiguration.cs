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
            Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(40);

            Property(s => s.Content)
                .IsRequired()
                .HasMaxLength(2000);

            HasOptional(s => s.User)
                .WithMany(m => m.ScrumTasks)
                .HasForeignKey(s => s.UserId);

            HasOptional(s => s.Estimation)
                .WithMany(e => e.ScrumTasksEstimated)
                .HasForeignKey(s => s.EstimationId);

            HasOptional(s => s.Took)
                .WithMany(e => e.ScrumTasksTaken)
                .HasForeignKey(s => s.TookId);
        }
    }
}