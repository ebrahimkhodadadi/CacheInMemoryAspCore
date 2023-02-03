
using System.ComponentModel.DataAnnotations;

namespace CacheService.Entity.Common;

public abstract class CacheBaseEntity
{
    public string Id { get; set; }

    [StringLength(80)]
    public string? CreatedBy { get; set; }
    public DateTime? Created { get; set; }

    [StringLength(80)]
    public string? ModifyBy { get; set; }
    public DateTime? Modified { get; set; }
}
