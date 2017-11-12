using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Core.Services
{
    public interface IScrumTasksService : IService
    {
        ScrumTask Create(ScrumTaskNewDto taskDto);
        ScrumTask Update(int taskId, ScrumTaskUpdateDto taskDto);
    }
}
