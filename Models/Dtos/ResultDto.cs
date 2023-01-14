namespace Models.Dtos;

public class ResultDto
{
    public List<Log> Logs { get; set; }
    public Character Winner { get; set; }

    public ResultDto() { }
}