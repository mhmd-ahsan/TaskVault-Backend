using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskVault.API.Dtos.TaskDtos;
using TaskVault.API.Repositories.Interfaces;

namespace TaskVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require JWT token
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _repo;

        public TaskController(ITaskRepository repo)
        {
            _repo = repo;
        }

        // Helper method to get current user ID from JWT
        private int GetUserId()
        {
            var userIdClaim = User.FindFirstValue("userId");
            return int.Parse(userIdClaim!);
        }


        // GET: api/Task
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var userId = GetUserId();
            var result = await _repo.GetTasksByUser(userId);
            return Ok(result);
        }

        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskDto dto)
        {
            var userId = GetUserId();
            var result = await _repo.AddTask(dto, userId);
            return Ok(result);
        }

        // PUT: api/Task
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDto dto)
        {
            var userId = GetUserId();
            var result = await _repo.UpdateTask(id, dto, userId);
            return Ok(result);
        }

        // DELETE: api/Task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = GetUserId();
            var result = await _repo.DeleteTask(id, userId);
            return Ok(result);
        }
        // PATCH: api/Task/complete/{id}
        [HttpPatch("complete/{id}")]
        public async Task<IActionResult> MArkCompleted(int id)
        {
            var userId = GetUserId();
            var task = await _repo.MarkTaskCompleted(id, userId);
            return Ok(task);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var userId = GetUserId();
            var summary = await _repo.GetDashboardSummary(userId);
            return Ok(summary);
        }

    }
}
