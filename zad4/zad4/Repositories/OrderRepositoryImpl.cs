using System.Data.SqlClient;
using zad4.Model;

namespace zad4.Repositories;

public class OrderRepositoryImpl : OrderRepository
{
    private IConfiguration _configuration;

    public OrderRepositoryImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Order> findByProductIdAndAmount(int productId, int amount)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM \"Order\" WHERE IdProduct = @IdProduct AND Amount = @Amount";
        cmd.Parameters.AddWithValue("@IdProduct", productId);
        cmd.Parameters.AddWithValue("@Amount", amount);
        
        var dr = cmd.ExecuteReader();

        var orders = new List<Order>();
        
        while (dr.Read())
        {
            var order = new Order()
            {
                IdOrder = (int)dr["IdOrder"],
                IdProduct = (int)dr["IdProduct"],
                Amount = (int)dr["Amount"],
                CreatedAt = (DateTime)dr["CreatedAt"],
                FulfilledAt = Convert.IsDBNull(dr["FulfilledAt"]) ? null : (DateTime?)dr["FulfilledAt"]
            };
            orders.Add(order);
        }

        return orders;
    }

    public int update(Order order)
    {
        return save(order, false);
    }

    public int create(Order order)
    {
        return save(order, true);
    }

    private int save(Order order, bool create)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@IdProduct", order.IdProduct);
        cmd.Parameters.AddWithValue("@Amount", order.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", order.CreatedAt);
        cmd.Parameters.AddWithValue("@FulfilledAt", order.FulfilledAt);
        
        if (create)
        {
            cmd.CommandText = "INSERT INTO \"Order\"(IdProduct, Amount, CreatedAt, FulfilledAt) " +
                              "VALUES(@IdProduct, @Amount, @CreatedAt, @FulfilledAt)";
        }
        else
        {
            cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
            
            cmd.CommandText = "UPDATE \"Order\" " +
                              "SET IdProduct = @IdProduct, Amount = @Amount, CreatedAt = @CreatedAt, FulfilledAt = @FulfilledAt " +
                              "WHERE IdOrder = @IdOrder";
        }

        return cmd.ExecuteNonQuery();
    }
}