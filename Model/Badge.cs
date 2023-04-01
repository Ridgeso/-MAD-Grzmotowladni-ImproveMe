using ImproveMe.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImproveMe.Model
{
    public class Badge
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        [ForeignKey(nameof(Challange))]
        public long ChallangeId { get; set; }

        public string Name { get; set; }

        public Rank Rank { get; set; }

    }
}
