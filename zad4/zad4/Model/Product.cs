using System.ComponentModel.DataAnnotations;

namespace zad4.Model;

public class Product
{
    [Required]
    public int IdProduct { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
}