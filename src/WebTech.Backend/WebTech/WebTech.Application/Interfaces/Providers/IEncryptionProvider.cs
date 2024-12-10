using WebTech.Application.DTOs;

namespace WebTech.Application.Interfaces.Providers;

public interface IEncryptionProvider
{
    Task<SaltAndHash> HashPasswordAsync(string password);

    Task<bool> VerifyPasswordHash(string password, SaltAndHash saltAndHash);
}