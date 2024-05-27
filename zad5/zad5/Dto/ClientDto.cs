using zad5.Models;

namespace zad5.Dto;

public class ClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }

    public static ClientDto Map(Client client)
    {
        return new ClientDto
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            Telephone = client.Telephone,
            Pesel = client.Pesel
        };
    }
    
    public Client MapToClient()
    {
        return new Client
        {
            FirstName = FirstName,  
            LastName = LastName,
            Email = Email,
            Telephone = Telephone,
            Pesel = Pesel
        };
    }
}