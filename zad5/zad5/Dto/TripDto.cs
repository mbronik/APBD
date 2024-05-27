using System.Security.Cryptography;
using zad5.Models;

namespace zad5.Dto;

public class TripDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public ICollection<CountryDto> Countries { get; set; }
    public ICollection<ClientDto> Clients { get; set; }

    public static TripDto Map(Trip trip)
    {
        return new TripDto()
        {
            Name = trip.Name,
            Description = trip.Description,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            MaxPeople = trip.MaxPeople,
            Countries = trip.IdCountries.Select(CountryDto.Map).ToList(),
            Clients = trip.ClientTrips.Select(c => ClientDto.Map(c.IdClientNavigation)).ToList()
        };
    }
}