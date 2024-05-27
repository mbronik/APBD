using Microsoft.EntityFrameworkCore;
using zad5.Models;

namespace zad5.Controlers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/client")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly ApbdContext _context;

    public ClientsController(ApbdContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        return await _context.Clients
            .OrderByDescending(c => c.FirstName)
            .ToListAsync();
    }

    
    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var client = await _context.Clients.FindAsync(idClient);
        if (client == null)
        {
            return NotFound();
        }

        var hasTrips = await _context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);
        if (hasTrips)
        {
            return BadRequest("Client has trips and cannot be deleted.");
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}