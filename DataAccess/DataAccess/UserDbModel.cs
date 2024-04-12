namespace UserTaskApi.DataAccess
{
    public class UserDbModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<UserTaskDdModel> Tasks { get; set; } = null!;
        public virtual ICollection<UserTaskHistoryDbModel> TaskHistory { get; set; } = null!;
    }
}
