using zad4.Model;

namespace zad4.Repositories;

public interface WarehouseRepository
{
    Warehouse? GetById(int id);
}