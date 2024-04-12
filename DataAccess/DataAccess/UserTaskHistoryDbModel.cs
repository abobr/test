namespace UserTaskApi.DataAccess
{
    public class UserTaskHistoryDbModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }

        public UserDbModel User { get; set; } = null!;

        public UserTaskDdModel Task { get; set; } = null!;
    }
}
