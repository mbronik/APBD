using zad4.DTO;
using zad4.Model;
using zad4.Repositories;

namespace zad4.Service;

public class WarehouseServiceImpl : WarehouseService
{
    private WarehouseRepository WarehouseRepository;
    private ProductRepository ProductRepository;
    private OrderRepository OrderRepository;
    private ProductWarehouseRepository ProductWarehouseRepository;

    public WarehouseServiceImpl(WarehouseRepository warehouseRepository, ProductRepository productRepository, OrderRepository orderRepository, ProductWarehouseRepository productWarehouseRepository)
    {
        WarehouseRepository = warehouseRepository;
        ProductRepository = productRepository;
        OrderRepository = orderRepository;
        ProductWarehouseRepository = productWarehouseRepository;
    }

    public int AddProductToWarehouse(ProductWarehouseDTO productWarehouseDto)
    {
        var product = ProductRepository.GetById(productWarehouseDto.IdProduct);

        if (product == null)
        {
            throw new ArgumentException("Product not exist");
        }
        
        var warehouse = WarehouseRepository.GetById(productWarehouseDto.IdWarehouse);

        if (warehouse == null)
        {
            throw new ArgumentException("Warehouse not exist");
        }

        if (productWarehouseDto.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        var orderList =
            OrderRepository.findByProductIdAndAmount(productWarehouseDto.IdProduct, productWarehouseDto.Amount);

        if (orderList.Count == 0)
        {
            throw new ArgumentException("Order not exist");
        }

        Order? order = null;
        
        foreach (var testOrder in orderList)
        {
            if (testOrder.CreatedAt > productWarehouseDto.CreatedAt)
            {
                continue;
            }
            
            var productWarehouse = ProductWarehouseRepository.GetByOrderId(testOrder.IdOrder);

            if (productWarehouse != null)
            {
                continue;
            }

            order = testOrder;
            break;
        }

        if (order == null)
        {
            throw new ArgumentException("Orders found do not meet the conditions");
        }
        
        order.FulfilledAt = DateTime.Now;
        OrderRepository.update(order);

        var newProductWarehouse = productWarehouseDto.Map();

        newProductWarehouse.IdProduct = product.IdProduct;
        newProductWarehouse.Price = product.Price * newProductWarehouse.Amount;
        newProductWarehouse.CreatedAt = DateTime.Now;
        
        return ProductWarehouseRepository.create(newProductWarehouse);
    }
}