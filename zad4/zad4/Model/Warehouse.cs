using System.ComponentModel.DataAnnotations;

namespace zad4.Model;

public class Warehouse
{
    [Required]
    public int IdWarehouse { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string Address { get; set; }
}