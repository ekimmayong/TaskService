using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Interfaces.BaseRepository;
using TaskService.Domain.Interfaces.IServices;

namespace TaskService.Domain.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;
        public IdentityService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public string GenerateMockToken()
        {
            var response = _identityRepository.GenerateMockToken();
            return response;
        }
    }
}
