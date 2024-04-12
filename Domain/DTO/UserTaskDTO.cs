namespace UserTaskApi.DTO
{
    public class UserTaskDTO
    {
        public required string Description { get; set; }

        public required string TaskState { get; set; }

        public required string AssignedUser { get; set; }
    }
}
