namespace Cw7.DTOs.Response;

public class PCComponentsResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ComponentResponseDto> Components { get; set; } = new();
}
