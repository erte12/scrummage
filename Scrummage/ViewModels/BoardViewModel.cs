using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.ViewModels
{
    public class BoardViewModel
    {
        public Team Team { get; set; }
        public Sprint Sprint { get; set; }
    }
}