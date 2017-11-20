using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Core.Services
{
    public interface IScrumTasksService : IService
    {
        ScrumTask Create(ScrumTask newScrumTask);
        ScrumTask Update(int taskId, ScrumTaskUpdateDto taskDto);
    }
}
