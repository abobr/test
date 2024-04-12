using DataAccess.Context;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using UserTaskApi.DataAccess;

namespace Domain.Repository
{
    public class UserTaskHistoryRepository(UserTaskContext context) : IUserTaskHistoryRepository
    {
        public async Task<Dictionary<int, List<UserTaskHistoryDbModel>>> GetTaskHistoryByTaskIds(IEnumerable<int> taskIds)
        {
            return await context.TasksHistory
                .Where(x => taskIds.Contains(x.TaskId))
                .GroupBy(o => o.TaskId)
                .ToDictionaryAsync(o => o.Key, o => o.ToList());
        }


        public async Task AddTaskHistoryBatchAsync(List<UserTaskHistoryDbModel> taskHistoryList, CancellationToken cancellationToken)
        {
            await context.TasksHistory.AddRangeAsync(taskHistoryList, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddTaskHistoryAsync(UserTaskDdModel task, CancellationToken cancellationToken)
        {
            if (!task.UserId.HasValue)
                return;
            var model = new UserTaskHistoryDbModel() { Task = task, User = task.AssignedUser };
            await context.TasksHistory.AddAsync(model, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

    }
}
