using UserTaskApi.DataAccess;

namespace Domain.Interface
{
    public interface IUserTaskHistoryRepository
    {
        public Task<Dictionary<int, List<UserTaskHistoryDbModel>>> GetTaskHistoryByTaskIds(IEnumerable<int> taskIds);

        public Task AddTaskHistoryBatchAsync(List<UserTaskHistoryDbModel> taskHistoryList, CancellationToken cancellationToken);

        public Task AddTaskHistoryAsync(UserTaskDdModel task, CancellationToken cancellationToken);
    }
}
