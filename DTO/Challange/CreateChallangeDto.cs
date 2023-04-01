using ImproveMe.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImproveMe.DTO.Challange
{
    public class CreateChallangeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ChallangeType Type { get; set; }

        public DateTime Start { get; set; }

        public long UserId { get; set; }
    }
}
