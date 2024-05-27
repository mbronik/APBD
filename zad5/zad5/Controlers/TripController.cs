using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zad5.Dto;
using zad5.Models;
using zad5.Services;

namespace zad5.Controlers;

[Route("api/trips")]
[ApiController]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetTrips()
    {
        return await _tripService.GetTrips();
    }
    
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTrip(int idTrip, [FromBody] ClientTripDto clientTripDto)
    {
        try
        {
            await _tripService.AddClientToTrip(idTrip, clientTripDto);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}