namespace Cw7.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PCs")]
public class PC
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public double Weight { get; set; }
    
    [Required]
    public int Warranty { get; set; }
    
    [Required]
    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public int Stock { get; set; }
    
    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}
