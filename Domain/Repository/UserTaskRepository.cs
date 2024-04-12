using DataAccess.Context;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using UserTaskApi.DataAccess;
using UserTaskApi.DTO;
using UserTaskApi.Enums;

namespace Domain.Repository
{
    public class UserTaskRepository(UserTaskContext context): IUserTaskRepository
    {
        public async Task<UserTaskDdModel> GetUserTaskByIdAsync(int id)
        {
            return await context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<UserTaskDdModel>> GetAllUserTasksAsync()
        {
            return await context.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<UserTaskDdModel>> GetUncomplitedTasksAsync(CancellationToken cancellationToken)
        {
            return await context.Tasks.Where(t => t.State != TaskStateType.Completed).ToListAsync(cancellationToken);
        }

        public async Task<UserTaskDdModel> AddTaskAsync(CreateTaskDTO createTaskDto, CancellationToken cancellationToken)
        {
            UserTaskDdModel task = new()
            {
                Description = createTaskDto.Description
            };

            if (context.Users.Any()) // Check if there is available user
            {
                task.AssignedUser = await context.Users.OrderBy(o => Guid.NewGuid()).FirstAsync(cancellationToken); // 
                task.State = TaskStateType.InProgress;
            }
            else
            {
                task.State = TaskStateType.Waiting;
            }

            await context.Tasks.AddAsync(task, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return task;
        }
    }
}
