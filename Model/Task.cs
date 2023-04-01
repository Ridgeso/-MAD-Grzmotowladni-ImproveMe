using ImproveMe.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImproveMe.Model
{
    public class Task
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
            
        public TaskType Type { get; set; }

        public DateOnly Start { get; set; }

        public DateOnly Checked { get; set; }

        public ulong  UserId { get; set; }

        public ulong BadgeId { get; set; }


    }
}
