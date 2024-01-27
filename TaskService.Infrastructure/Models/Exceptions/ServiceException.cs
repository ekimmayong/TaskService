using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Infrastructure.Models.Exceptions
{
    [Serializable]
    public class ServiceException: Exception
    {
        public ServiceException(){ }
        public ServiceException(string messge): base(messge){}
        public ServiceException(string message, Exception inner)
            : base(message, inner){}
        public required int StatusCode { get; set; }
    }
}
