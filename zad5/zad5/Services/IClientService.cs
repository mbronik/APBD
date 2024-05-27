using Microsoft.AspNetCore.Mvc;
using zad5.Models;

namespace zad5.Services;

public interface IClientService
{ 
    Task<int> DeleteClient(int idClient);
}