using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserTaskApi.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
