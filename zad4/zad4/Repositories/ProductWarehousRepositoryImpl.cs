using System.Data.SqlClient;
using zad4.Model;

namespace zad4.Repositories;

public class ProductWarehouseRepositoryImpl : ProductWarehouseRepository
{
    private IConfiguration _configuration;

    public ProductWarehouseRepositoryImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ProductWarehouse? GetByOrderId(int OrderId)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Product_Warehouse WHERE IdOrder = @IdOrder ORDER BY CreatedAt ASC";
        cmd.Parameters.AddWithValue("@IdOrder", OrderId);
        
        var dr = cmd.ExecuteReader();
        
        if (!dr.Read()) return null;
        
        var productWarehouse = new ProductWarehouse()
        {
            IdProductWarehouse = (int)dr["IdProductWarehouse"],
            IdWarehouse = (int)dr["IdWarehouse"],
            IdProduct = (int)dr["IdProduct"],
            IdOrder = (int)dr["IdOrder"],
            Amount = (int)dr["Amount"],
            Price = (decimal)dr["Price"],
            CreatedAt = (DateTime)dr["CreatedAt"]
        };

        return productWarehouse;
    }

    public int create(ProductWarehouse productWarehouse)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                          "VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt); SELECT SCOPE_IDENTITY();";
        
        cmd.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
        cmd.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
        cmd.Parameters.AddWithValue("@IdOrder", productWarehouse.IdOrder);
        cmd.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
        cmd.Parameters.AddWithValue("@Price", productWarehouse.Price);
        cmd.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);
        
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}