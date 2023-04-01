using ImproveMe.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImproveMe.DTO.Badge
{
    public class CreateBadgeDto
    {
        public long ChallangeId { get; set; }

        public string Name { get; set; }

        public Rank Rank { get; set; }
    }
}
