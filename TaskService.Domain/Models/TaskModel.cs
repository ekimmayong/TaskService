using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Domain.Models
{
    public class TaskModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CompletedTimeStamp { get; set; }
        public DateTime UpdateTimeStamp { get; set; } = DateTime.UtcNow;
    }
}
