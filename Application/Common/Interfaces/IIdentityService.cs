using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task CreateUserAsync(UserModel model, CancellationToken cancellationToken);
    }
}
