using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threelab.Domain.Response
{
    public class AccessResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public UserInfoResponse User { get; set; }
    }
}
