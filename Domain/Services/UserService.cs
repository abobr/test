using Domain.Interface;
using UserTaskApi.DTO;

namespace Domain.Services
{
    public class UserService(IUserRepository userRepository): IUserService
    {
        public async Task AddUserAsync(UserDTO userDto)
        {
            await userRepository.AddUserDTOAsync(userDto);
        }

        public async Task<UserDTO?> GetUserAsync(int id)
        {
            var user = await userRepository.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserDTO { Id = user.Id, Name = user.Name };
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return (await userRepository.GetUsers()).Select(x => new UserDTO() { Name = x.Name });
        }
    }
}
