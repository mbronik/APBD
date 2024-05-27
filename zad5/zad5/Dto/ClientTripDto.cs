using zad5.Models;

namespace zad5.Dto;

public class ClientTripDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }
    public int IdTrip { get; set; }
    public string TripName { get; set; }
    public DateTime PaymentDate { get; set; }

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
