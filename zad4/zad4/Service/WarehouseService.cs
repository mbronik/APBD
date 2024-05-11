using zad4.DTO;

namespace zad4.Service;

public interface WarehouseService
{
    int AddProductToWarehouse(ProductWarehouseDTO productWarehouseDto);
}