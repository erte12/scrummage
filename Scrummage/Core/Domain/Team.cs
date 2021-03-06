﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Scrummage.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<ApplicationUser> WaitingUsers { get; set; }

        public List<ApplicationUser> UsersWithThisAsDefault { get; set; }

        public List<Sprint> Sprints { get; set; }

        public ApplicationUser ScrumMaster { get; set; }

        public string ScrumMasterId { get; set; }

        public int NumberOfMembers => Users.Count + 1; //plus Scrum Master

        public IEnumerable<ApplicationUser> UsersWithScrumMaster => 
            Users.Concat(new List<ApplicationUser>{ScrumMaster});

        public Team()
        {
            Users = new List<ApplicationUser>();
            WaitingUsers = new List<ApplicationUser>();
            Sprints = new List<Sprint>();
        }

        public bool HasAnySprint => Sprints.Count > 0;
    }
}