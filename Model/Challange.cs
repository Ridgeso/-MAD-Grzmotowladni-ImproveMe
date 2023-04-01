using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImproveMe.Model;

public class Challange
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
            
    public ChallangeType Type { get; set; }

    public DateTime Start { get; set; }

    public DateTime Checked { get; set; }
        
    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
        
    [ForeignKey(nameof(Badge))]
    public long BadgeId { get; set; }

}