﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Scrummage.Models.Configuration
{
    public class TeamsConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamsConfiguration()
        {
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(60);

            HasMany(t => t.Users)
                .WithMany(u => u.Teams)
                .Map(cs =>
                {
                    cs.MapLeftKey("TeamId");
                    cs.MapRightKey("MemberId");
                    cs.ToTable("MemberTeam");
                });

            HasMany(t => t.WaitingUsers)
                .WithMany(u => u.RequestedTeams)
                .Map(cs =>
                {
                    cs.MapLeftKey("TeamId");
                    cs.MapRightKey("MemberId");
                    cs.ToTable("JoinTeamRequests");
                });

            HasRequired(t => t.ScrumMaster)
                .WithMany(u => u.ManagedTeams)
                .HasForeignKey(t => t.ScrumMasterId)
                .WillCascadeOnDelete(false);
        }
    }
}