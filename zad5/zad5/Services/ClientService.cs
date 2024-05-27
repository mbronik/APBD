using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zad5.Models;

namespace zad5.Services;

public class ClientService : IClientService
{
    private readonly ApbdContext _context;
    
    public ClientService(ApbdContext context)
    {
        _context = context;
    }

    public async Task<int> DeleteClient(int idClient)
    {
        var client = await _context.Clients.FindAsync(idClient);
        if (client == null)
        {
            throw new ArgumentException("Client not exists");
        }

        var hasTrips = await _context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);
        if (hasTrips)
        {
            throw new ArgumentException("Client has trips");
        }

        _context.Clients.Remove(client);
        return await _context.SaveChangesAsync();
    }
}