using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cw7.Entities;
[Table("ComponentTypes")]
public class ComponentType
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string Abbreviation { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Component> Components { get; set; } = new List<Component>();
}
