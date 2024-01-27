using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Infrastructure.Configurations
{
    public class JwtConfiguration
    {
        public const string ConfigurationName = "JWT";

        public required string ValidIssuer { get; set; }
        public required string ValidAudience { get; set; }
        public required string  SecretKey { get; set; }
    }
}
