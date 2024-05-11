using System.ComponentModel.DataAnnotations;
using zad4.Model;

namespace zad4.DTO;

public class ProductWarehouseDTO
{
    [Required]
    public int IdProduct { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }

    public ProductWarehouse Map()
    {
        return new ProductWarehouse()
        {
            IdOrder = IdProduct,
            IdWarehouse = IdWarehouse,
            Amount = Amount,
            CreatedAt = CreatedAt
        };
    }
}