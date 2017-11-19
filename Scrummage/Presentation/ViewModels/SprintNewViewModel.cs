﻿using System;
using System.ComponentModel.DataAnnotations;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.ViewModels
{
    public class SprintNewViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int TeamId { get; set; }

        public TeamDto Team { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }
    }
}