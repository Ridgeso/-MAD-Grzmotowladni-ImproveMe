using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImproveMe.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public uint Points { get; set; }

        public ushort Level { get; set; }

        public DateTime LastLogged { get; set; }
            
        public uint Streak { get; set;} 
    }
}
