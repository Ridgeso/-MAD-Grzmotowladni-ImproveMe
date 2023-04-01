using ImproveMe.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImproveMe.Model
{
    public class Badge
    {
        public ulong Id { get; set; }
        public ulong TaskId { get; set; }

        public string Name { get; set; }

        public Rank Rank { get; set; }

    }
}
