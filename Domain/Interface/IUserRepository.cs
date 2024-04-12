using UserTaskApi.DataAccess;
using UserTaskApi.DTO;

namespace Domain.Interface
{
    public interface IUserRepository
    {
        public Task AddUserDTOAsync(UserDTO user);

        public Task<List<int>> GetUserIdsAsync(CancellationToken cancellationToken);

        public Task<UserDbModel> FindByIdAsync(int id);

        public Task<IEnumerable<UserDbModel>> GetUsers();
    }
}
