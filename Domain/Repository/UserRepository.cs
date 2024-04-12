using DataAccess.Context;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using UserTaskApi.DataAccess;
using UserTaskApi.DTO;

namespace Domain.Repository
{
    public class UserRepository(UserTaskContext context): IUserRepository
    {
        public async Task AddUserDTOAsync(UserDTO user)
        {
            UserDbModel dbUser = new() { Name = user.Name };
            await context.Users.AddAsync(dbUser);
            await context.SaveChangesAsync();
        }

        public async Task<List<int>> GetUserIdsAsync(CancellationToken cancellationToken)
        {
            return await context.Users.Select(x => x.Id).ToListAsync(cancellationToken);
        }

        public async Task<UserDbModel> FindByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<UserDbModel>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }
    }
}
