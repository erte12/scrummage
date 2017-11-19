using System;
using System.ComponentModel.DataAnnotations;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.ViewModels
{
    public class SprintNewViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TeamId { get; set; }

        public TeamDto Team { get; set; }

        [Required(ErrorMessage = "Starting date is required.")]
        public DateTime StartsAt { get; set; }

        [Required(ErrorMessage = "Ending date is required.")]
        public DateTime EndsAt { get; set; }
    }
}