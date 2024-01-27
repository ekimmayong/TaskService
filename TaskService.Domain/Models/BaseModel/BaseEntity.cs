using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Domain.Models.BaseModel
{
    public class BaseEntity
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public DateTime UpdateTimeStamp { get; set; } = DateTime.UtcNow;
    }
}
