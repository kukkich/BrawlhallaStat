using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Domain.Games;

[PrimaryKey(nameof(AuthorId), nameof(GameDetailsId), nameof(ReplayFileId))]
public class Game
{
    public string AuthorId { get; set; } = null!;
    public User Author { get; set; } = null!;
    
    public string GameDetailsId { get; set; } = null!;
    public GameDetail Detail { get; set; } = null!;
    
    public string ReplayFileId { get; set; } = null!;
    public ReplayFile ReplayFile { get; set; } = null!;
}
