using System.ComponentModel.DataAnnotations;

namespace PickyPrincessDb.entities;

public abstract class BaseEntity
{
    [Key] 
    public Guid Id { get; set; }
}