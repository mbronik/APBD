using zad4.Model;

namespace zad4.Repositories;

public interface ProductWarehouseRepository
{
    ProductWarehouse? GetByOrderId(int OrderId);
    int create(ProductWarehouse productWarehouse);
}