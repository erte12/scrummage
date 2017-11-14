using System;
using System.ComponentModel.DataAnnotations;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Presentation.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(200)]
        public string Content { get; set; }
            
        public TeamDto Team { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime? EndsAt { get; set; }
    }
}