namespace Models.Dtos;

public class ResultDto
{
    public List<Log> Logs { get; set; }
    public Winner Winner { get; set; }

    public ResultDto() { }
}