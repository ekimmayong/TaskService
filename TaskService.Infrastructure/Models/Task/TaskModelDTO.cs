using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Infrastructure.Models.BaseModel;

namespace TaskService.Infrastructure.Models.Task
{
    public class TaskModelDTO: BaseEntity
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CompletedTimeStamp { get; set; }
    }
}
