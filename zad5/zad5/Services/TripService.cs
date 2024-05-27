using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zad5.Dto;
using zad5.Models;

namespace zad5.Services;

public class TripService : ITripService
{
    private readonly ApbdContext _context;

    public TripService(ApbdContext context)
    {
        _context = context;
    }
    
    public async Task<ActionResult<IEnumerable<TripDto>>> GetTrips()
    {
        return await _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .Select(t => TripDto.Map(t))
            .ToListAsync();
    }

    public async Task<int> AddClientToTrip(int idTrip, ClientTripDto clientTripDto)
    {
        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Pesel == clientTripDto.Pesel);
        if (client == null)
        {
            client = clientTripDto.MapToClient();
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        var trip = await _context.Trips.FindAsync(idTrip);
        if (trip == null)
        {
            throw new ArgumentException("Trip not exists");
        }

        var clientTripExists = await _context.ClientTrips
            .AnyAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == idTrip);
        if (clientTripExists)
        {
            throw new ArgumentException("Client is already registered for this trip.");
        }

        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            RegisteredAt = DateTime.Now
        };

        _context.ClientTrips.Add(clientTrip);
        return await _context.SaveChangesAsync();
    }
}