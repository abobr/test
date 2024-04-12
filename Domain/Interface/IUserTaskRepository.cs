using UserTaskApi.DataAccess;
using UserTaskApi.DTO;

namespace Domain.Interface
{
    public interface IUserTaskRepository
    {
        public Task<UserTaskDdModel> GetUserTaskByIdAsync(int id);

        public Task<IEnumerable<UserTaskDdModel>> GetAllUserTasksAsync();

        public Task<IEnumerable<UserTaskDdModel>> GetUncomplitedTasksAsync(CancellationToken cancellationToken);

        public Task<UserTaskDdModel> AddTaskAsync(CreateTaskDTO createTaskDto, CancellationToken cancellationToken);
    }
}
