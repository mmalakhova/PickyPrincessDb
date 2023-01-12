using System.ComponentModel.DataAnnotations.Schema;

namespace PickyPrincessDb.entities;

[Table("contender")]
public class Contender : BaseEntity
{
    public string Name { get; set; }
    public int Rating { get; set; }
    public int OrderIdx { get; set; }
    public Attempt Attempt { get; set; }
}