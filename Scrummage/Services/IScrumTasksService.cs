using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Models;

namespace Scrummage.Services
{
    public interface IScrumTasksService
    {
        void UpdateUser(int userId);
        void UpdateEstimation(int estimationId);
        void UpdatePriority(int priority);
        void UpdateTaskType(TaskType taskType, int? tookId);
    }
}
