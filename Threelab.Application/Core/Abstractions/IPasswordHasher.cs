using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threelab.Application.Core.Abstractions
{
    public interface IPasswordHasher
    {
        string GeneratePassword(string password);
        bool VerifyPassword(string passwordHash, string inputPassword);
    }
}
