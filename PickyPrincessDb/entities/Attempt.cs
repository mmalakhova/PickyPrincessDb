using System.ComponentModel.DataAnnotations.Schema;

namespace PickyPrincessDb.entities;

[Table("attempt")]
public class Attempt : BaseEntity
{
    public string Name { get; set; }
    public int Count { get; set; }
    public int HappyLevel { get; set; }
    public List<Contender> Contenders { get; set; } = new();
}