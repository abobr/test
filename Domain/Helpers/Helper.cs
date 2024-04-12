using System.Threading.Tasks;
using System;

namespace UserTaskApi.Helpers
{
    public static class Helper
    {
        public static int GenerateRandomUserId(List<int> usersIds, int previous_user_id)
        {
            var random = new Random();
            // Tasks should be reassigned to another random user
            var randomUserId = usersIds[random.Next(usersIds.Count)];

            while (randomUserId == previous_user_id)
            { // if pick the same user, just run random again until we get another user.
                randomUserId = usersIds[random.Next(usersIds.Count)];
            }
            return randomUserId;
        }
    }
}
