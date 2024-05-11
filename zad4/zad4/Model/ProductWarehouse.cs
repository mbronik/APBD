using System.ComponentModel.DataAnnotations;

namespace zad4.Model;

public class ProductWarehouse
{
    [Required]
    public int IdProductWarehouse { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    [Required]
    public int IdProduct { get; set; }
    [Required]
    public int IdOrder { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}
