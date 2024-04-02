namespace BrawlhallaStat.Api.General.Paging;

public class Page
{
    public required int Number { get; set; }
    public required int Size { get; set; }

    public int GetOffset() => (Number - 1) * Size;
}