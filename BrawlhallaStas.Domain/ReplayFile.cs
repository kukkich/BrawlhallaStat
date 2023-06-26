using System.ComponentModel.DataAnnotations;

namespace BrawlhallaStat.Domain;

public class ReplayFile
{
    [Key] 
    public string Id { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public byte[] FileData { get; set; } = null!;
}