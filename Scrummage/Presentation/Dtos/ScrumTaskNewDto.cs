﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.Dtos
{
    public class ScrumTaskNewDto
    {
        public string Content { get; set; }

        public int SprintId { get; set; }
    }
}