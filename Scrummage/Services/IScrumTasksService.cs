using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Services
{
    public interface IScrumTasksService
    {
        ScrumTask Create(NewScrumTaskDto taskDto);
        ScrumTask Update(int taskId, UpdateScrumTaskDto taskDto);
    }
}
