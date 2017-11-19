using System;
using System.ComponentModel.DataAnnotations;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.ViewModels
{
    public class SprintNewViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must contain at least 3 characters.")]
        [MaxLength(60, ErrorMessage = "Name must contain less than 60 characters.")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Description must contain less than 1000 characters.")]
        public string Description { get; set; }

        public int TeamId { get; set; }

        public TeamDto Team { get; set; }

        [Required(ErrorMessage = "Starting date is required.")]
        public DateTime StartsAt { get; set; }

        [Required(ErrorMessage = "Ending date is required.")]
        public DateTime EndsAt { get; set; }
    }
}