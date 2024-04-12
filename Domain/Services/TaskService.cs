using Domain.Interface;
using Domain.Repository;
using Microsoft.Extensions.Logging;
using UserTaskApi.DataAccess;
using UserTaskApi.DTO;
using UserTaskApi.Enums;
using UserTaskApi.Helpers;

namespace UserTaskApi.Services
{
    public class TaskService(
        ILogger<TaskService> logger,
        IUserRepository userRepository,
        IUserTaskRepository userTaskRepository,
        IUserTaskHistoryRepository userTaskHistoryRepository) : ITaskService
    {

        public async Task AddTaskAsync(CreateTaskDTO createTaskDto, CancellationToken cancellationToken)
        {
            var task = await userTaskRepository.AddTaskAsync(createTaskDto, cancellationToken);
            await userTaskHistoryRepository.AddTaskHistoryAsync(task, cancellationToken);
        }

        public async Task<IEnumerable<UserTaskDTO>> GetTasksDtoAsync()
        {
            var response = (await userTaskRepository.GetAllUserTasksAsync()).Select(x => new UserTaskDTO
            {
                TaskState = x.State.ToString(),
                AssignedUser = x.AssignedUser.Name.ToString(),
                Description = x.Description
            });

            return response;
        }

        public async Task<UserTaskDdModel?> GetUserTaskByIdAsync(int id)
        {
            return await userTaskRepository.GetUserTaskByIdAsync(id);
        }


        // In this task I just do reassign all the task. 
        // For highload system, I will rather do it in batches, that will not overload the system. 
        // (Like 1000 Task each 30 seconds etc) 
        public async Task ReassignTasksAsync(CancellationToken cancellationToken)
        {
            var usersIds = await userRepository.GetUserIdsAsync(cancellationToken);
            if (usersIds.Count == 0)
            {
                return;
            }

            var tasks = await userTaskRepository.GetUncomplitedTasksAsync(cancellationToken);

            var taskHistoryDict = await userTaskHistoryRepository.GetTaskHistoryByTaskIds(tasks.Select(task => task.Id));
            // id - taskId, 
            List<UserTaskHistoryDbModel> taskHistoryBatch = [];
            foreach (var task in tasks)
            {
                if (usersIds.Count == 1)
                {
                    task.State = TaskStateType.Waiting;
                    logger.Log(LogLevel.Information, "Task {} waiting", task.Id);
                    continue;
                }

                if (IsTaskComplete(usersIds, taskHistoryDict, task))
                { 
                    task.State = TaskStateType.Completed;
                    task.UserId = null; // 
                    logger.Log(LogLevel.Information, "Task {} completed", task.Id);
                    continue;
                }

                task.UserId = Helper.GenerateRandomUserId(usersIds, task.UserId.Value);
                logger.Log(LogLevel.Information, "Task {} assigned to the user {}", task.Id, task.UserId.Value);
                taskHistoryBatch.Add(new UserTaskHistoryDbModel() { TaskId = task.Id, UserId = task.UserId.Value });
            }
            await userTaskHistoryRepository.AddTaskHistoryBatchAsync(taskHistoryBatch, cancellationToken);
        }

        // The task has been transferred at least 3 times and to all existing users
        private static bool IsTaskComplete(List<int> usersIds, Dictionary<int, List<UserTaskHistoryDbModel>> taskHistoryDict, UserTaskDdModel task)
        {
            return taskHistoryDict.TryGetValue(task.Id, out List<UserTaskHistoryDbModel>? value)
                                && usersIds.All(userId => value.Any(x => x.UserId == userId)) && value.Count >= 3;
        }
    }
}
