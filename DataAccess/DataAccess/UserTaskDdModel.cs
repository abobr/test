using UserTaskApi.Enums;

namespace UserTaskApi.DataAccess
{
    public class UserTaskDdModel
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public TaskStateType State { get; set; }
        public int? UserId{ get; set; }
        public UserDbModel AssignedUser { get; set; } = null!;
        
        public virtual ICollection<UserTaskHistoryDbModel>? TaskHistory { get; set; }
    }
}
