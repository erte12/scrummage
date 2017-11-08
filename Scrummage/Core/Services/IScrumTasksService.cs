using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Core.Services
{
    public interface IScrumTasksService
    {
        ScrumTask Create(NewScrumTaskDto taskDto);
        ScrumTask Update(int taskId, UpdateScrumTaskDto taskDto);
    }
}
