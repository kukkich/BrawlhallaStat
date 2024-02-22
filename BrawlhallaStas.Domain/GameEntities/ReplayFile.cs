using System.ComponentModel.DataAnnotations;
using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.GameEntities;

public class ReplayFile : IHaveId<string>
{
    [Key]
    public string Id { get; set; } = null!;

    public string FileName { get; set; } = null!;
    public byte[] FileData { get; set; } = null!;
}