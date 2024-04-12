using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using UserTaskApi.DataAccess;
using UserTaskApi.DTO;

namespace UserTaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTasksController(ITaskService service) : ControllerBase
    {
        // GET: api/UserTasks
        [HttpGet]
        public async Task<IEnumerable<UserTaskDTO>> GetTasks()
        {
            var tasks = await service.GetTasksDtoAsync();
            return tasks;
        }

        // GET: api/UserTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTaskDdModel>> GetUserTask(int id)
        {
            var userTask = await service.GetUserTaskByIdAsync(id);

            if (userTask == null)
            {
                return NotFound();
            }

            return userTask;
        }

        // POST: api/UserTasks
        [HttpPost]
        public async Task<ActionResult<UserTaskDdModel>> PostUserTask(CreateTaskDTO userTask, CancellationToken cancellationToken)
        {
            await service.AddTaskAsync(userTask, cancellationToken);

            return Ok();
        }
    }
}
