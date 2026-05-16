namespace Cw7.DTOs.Request;
using System.ComponentModel.DataAnnotations;

public class PCRequestDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Range(0.1, double.MaxValue)]
    public double Weight { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int Warranty { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
