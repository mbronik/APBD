using Microsoft.AspNetCore.Mvc;
using zad5.Dto;
using zad5.Models;

namespace zad5.Services;

public interface ITripService
{
    Task<ActionResult<IEnumerable<TripDto>>> GetTrips();
    Task<int> AddClientToTrip(int idTrip, ClientTripDto dto);
}