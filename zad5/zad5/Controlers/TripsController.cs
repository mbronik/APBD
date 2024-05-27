using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zad5.Dto;
using zad5.Models;

namespace zad5.Controlers;

[Route("api/trip")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ApbdContext _context;

    public TripsController(ApbdContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
    {
        return await _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .ToListAsync();
    }
    
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTrip(int idTrip, [FromBody] ClientTripDto dto)
    {
        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Pesel == dto.Pesel);
        if (client == null)
        {
            client = new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Pesel = dto.Pesel
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        var trip = await _context.Trips.FindAsync(idTrip);
        if (trip == null)
        {
            return NotFound("Trip not found.");
        }

        var clientTripExists = await _context.ClientTrips
            .AnyAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == idTrip);
        if (clientTripExists)
        {
            return BadRequest("Client is already registered for this trip.");
        }

        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            RegisteredAt = DateTime.Now
        };

        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();

        return Ok(clientTrip);
    }
}