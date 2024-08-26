using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threelab.Domain.Response
{
    public class UserInfoResponse
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Roles { get; set; }
    }
}
