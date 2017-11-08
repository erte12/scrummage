using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Models;

namespace Scrummage.Core.Services
{
    public interface ISprintService
    {
        Sprint Create(Sprint sprintToCreate);
    }
}
