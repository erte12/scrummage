using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Core.Services
{
    public interface ISprintService : IService
    {
        Sprint Create(Sprint sprintToCreate);
        Sprint Update(int id, SprintDto sprintDto);
    }
}
