namespace DTOs;

public class GameDto
{
    public Guid MazeUid { get; set;}
    public Guid GameUid { get; set;}
    public bool Completed { get; set;}
    public int CurrentPositionX { get; set;}
    public int CurrentPositionY { get; set;}
}