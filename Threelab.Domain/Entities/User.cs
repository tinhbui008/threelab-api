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
        public bool Verify { get; set; } = false;
    }
}