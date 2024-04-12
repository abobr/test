using UserTaskApi.DTO;

namespace Domain.Interface
{
    public interface IUserService
    {
        public Task AddUserAsync(UserDTO userDto);

        public Task<UserDTO?> GetUserAsync(int id);

        public Task<IEnumerable<UserDTO>> GetUsers();
    }
}
