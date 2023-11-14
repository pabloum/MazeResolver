namespace DTOs;

public class MazeDetailsDto
{
    public Guid MazeUid { get; set;}
    public int Height { get; set;}
    public int Width { get; set;}
    public IEnumerable<MazeBlockViewDto> Blocks { get; set; } = Enumerable.Empty<MazeBlockViewDto>();
}