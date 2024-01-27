using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Infrastructure.Models.BaseModel
{
    public class BaseEntity
    {
        public string RowVersion { get; set; }
        public DateTime UpdateTimeStamp { get; set; } = DateTime.UtcNow;
    }
}
