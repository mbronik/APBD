using System;

namespace LegacyApp;

public interface UserCreditService : IDisposable
{
    int GetCreditLimit(string lastName, DateTime dateOfBirth);
}