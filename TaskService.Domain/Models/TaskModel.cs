using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Models.BaseModel;

namespace TaskService.Domain.Models
{
    public class TaskModel: BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CompletedTimeStamp { get; set; }
    }
}
