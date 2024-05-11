using zad4.Model;

namespace zad4.Repositories;

public interface ProductRepository
{
    Product? GetById(int id);
}