namespace DTOs;

public class GameLookDto
{
    public GameDto Game { get; set; } = new GameDto();
    public MazeBlockViewDto MazeBlockView { get; set; } = new MazeBlockViewDto();
}