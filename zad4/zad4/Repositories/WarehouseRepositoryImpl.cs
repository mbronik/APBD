using zad4.Model;

namespace zad4.Repositories;

using System.Data.SqlClient;

public class WarehouseRepositoryImpl : WarehouseRepository
{
    private IConfiguration _configuration;
    
    public WarehouseRepositoryImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    // private readonly string _connectionString;
    //
    // public WarehouseRepository(IConfiguration configuration)
    // {
    //     _connectionString = configuration.GetConnectionString("DefaultConnection");
    // }

    // public async Task<Warehouse> GetWarehouseById(int id)
    // {
    //     using (var connection = new SqlConnection(_connectionString))
    //     {
    //         await connection.OpenAsync();
    //
    //         var query = "SELECT * FROM Warehouse WHERE IdWarehouse = @Id";
    //
    //         using (var command = new SqlCommand(query, connection))
    //         {
    //             command.Parameters.AddWithValue("@Id", id);
    //
    //             using (var reader = await command.ExecuteReaderAsync())
    //             {
    //                 if (await reader.ReadAsync())
    //                 {
    //                     return new Warehouse
    //                     {
    //                         IdWarehouse = reader.GetInt32(0),
    //                         Name = reader.GetString(1),
    //                         Address = reader.GetString(2)
    //                     };
    //                 }
    //             }
    //         }
    //     }
    //
    //     return null; // Jeśli nie znaleziono magazynu o podanym id
    // }
    
    public Warehouse? GetById(int id)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", id);
        
        var dr = cmd.ExecuteReader();
        
        if (!dr.Read()) return null;
        
        var warehouse = new Warehouse()
        {
            IdWarehouse = (int)dr["IdWarehouse"],
            Name = (string)dr["Name"],
            Address = (string)dr["Address"]
        };

        return warehouse;
    }
}
