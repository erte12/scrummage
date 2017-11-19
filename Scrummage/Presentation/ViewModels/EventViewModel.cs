using System;
using System.ComponentModel.DataAnnotations;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Presentation.ViewModels
{
    public class EventViewModel
    {
        public TeamDto Team { get; set; }
    }
}