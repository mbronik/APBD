using zad4.Model;

namespace zad4.Repositories;

public interface OrderRepository
{
    List<Order> findByProductIdAndAmount(int productId, int amount);
    int update(Order order);
    int create(Order order);
}