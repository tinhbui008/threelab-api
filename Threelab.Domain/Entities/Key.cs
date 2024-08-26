using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threelab.Domain.Base;

namespace Threelab.Domain.Entities
{
    public class Key : BaseEntity<Guid>
    {
        public string RefreshToken { get; set; }
    }
}
