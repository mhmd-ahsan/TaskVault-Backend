using TaskVault.API.Dtos.DashBoardDto;
using TaskVault.API.Dtos.TaskDtos;
using TaskVault.API.Helpers;
using TaskVault.API.Models;

namespace TaskVault.API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<HelperResponse> AddTask(TaskDto dto, int userId);
        Task<IEnumerable<TaskItem>> GetTasksByUser(int userId);
        Task<HelperResponse> UpdateTask(int taskId, TaskDto dto, int userId);
        Task<HelperResponse> DeleteTask(int taskId, int userId);
        Task<HelperResponse> MarkTaskCompleted(int taskId, int userId);

        // Dashboard
        Task<DashboardSummaryDto> GetDashboardSummary(int userId);

    }
}
