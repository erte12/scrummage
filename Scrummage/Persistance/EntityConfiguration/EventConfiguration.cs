using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Scrummage.Core.Domain;

namespace Scrummage.Persistance.EntityConfiguration
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}