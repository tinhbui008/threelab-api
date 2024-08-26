using System.ComponentModel.DataAnnotations.Schema;
using Threelab.Domain.Base;

namespace Threelab.Domain.Entities
{
    [Table("User")]
    public partial class User : BaseEntity<Guid>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public string Roles { get; set; } = RolesEnum.USER.ToString();
        public bool Verify { get; set; } = false;
    }

    public enum RolesEnum
    {
        DEV,
        USER,
        SUPERADMIN,
        ADMIN
    }
}