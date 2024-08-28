using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threelab.Domain.Base;

namespace Threelab.Domain.Entities
{
    public class KeyToken : BaseEntity<Guid>
    {
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
