namespace Cw7.DTOs.Response;

public class ComponentResponseDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Amount { get; set; }
    public string ComponentType { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
}
