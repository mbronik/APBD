using System.Data.SqlClient;
using zad4.Model;

namespace zad4.Repositories;

public class ProductRepositoryImpl : ProductRepository
{
    private IConfiguration _configuration;

    public ProductRepositoryImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Product? GetById(int id)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Product WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", id);
        
        var dr = cmd.ExecuteReader();
        
        if (!dr.Read()) return null;
        
        var product = new Product()
        {
            IdProduct = (int)dr["IdProduct"],
            Name = (string)dr["Name"],
            Description = (string)dr["Description"],
            Price = (decimal)dr["Price"]
        };

        return product;
    }
}