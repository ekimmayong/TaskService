using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Domain.Interfaces.BaseRepository
{
    public interface IIdentityRepository
    {
        string GenerateMockToken();
    }
}
