using Microsoft.EntityFrameworkCore;
using TaskVault.API.Data;
using TaskVault.API.Dtos.DashBoardDto;
using TaskVault.API.Dtos.TaskDtos;
using TaskVault.API.Helpers;
using TaskVault.API.Models;
using TaskVault.API.Repositories.Interfaces;

namespace TaskVault.API.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<HelperResponse> AddTask(TaskDto dto, int userId)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId,
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return new HelperResponse
            {
                Success = true,
                Message = "Task added successfully",
                Data = task
            };
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByUser(int userId)
        {
            return await _context.TaskItems.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<HelperResponse> UpdateTask(int taskId, TaskDto dto, int userId)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                return new HelperResponse
                {
                    Success = false,
                    Message = "Task not found"
                };

            task.Title = dto.Title;
            task.Description = dto.Description;
            await _context.SaveChangesAsync();

            return new HelperResponse
            {
                Success = true,
                Message = "Task Updated successfully!"
            };
        }

        public async Task<HelperResponse> DeleteTask(int taskId, int userId)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(t =>t.Id == taskId);
            if (task == null)
                return new HelperResponse
                {
                    Success = false,
                    Message = "Task not found"
                };

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return new HelperResponse
            {
                Success = true,
                Message = "Task deleted successfully"
            };
        }

        public async Task<HelperResponse> MarkTaskCompleted(int taskId, int userId)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
            if (task == null)
                return new HelperResponse
                {
                    Success = false,
                    Message = "Task not found"
                };

            task.IsCompleted = true;
            await _context.SaveChangesAsync();

            return new HelperResponse { Success = true, Message = "Task marked as completed!" };
        }

        public async Task<DashboardSummaryDto> GetDashboardSummary(int userId)
        {
            var tasks = await _context.TaskItems
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return new DashboardSummaryDto
            {
                TotalTasks = tasks.Count,
                CompletedTasks = tasks.Count(t => t.IsCompleted),
                PendingTasks = tasks.Count(t => !t.IsCompleted)
            };
        }


    }
}
