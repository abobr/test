using UserTaskApi.DataAccess;
using UserTaskApi.DTO;

namespace Domain.Interface
{
    public interface ITaskService
    {
        public Task AddTaskAsync(CreateTaskDTO createTaskDto, CancellationToken cancellationToken);

        public Task<IEnumerable<UserTaskDTO>> GetTasksDtoAsync();

        public Task<UserTaskDdModel?> GetUserTaskByIdAsync(int id);

        public Task ReassignTasksAsync(CancellationToken cancellationToken);
    }
}
