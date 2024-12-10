﻿using System.Security.Cryptography;
using System.Text;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Providers;

namespace WebTech.Application.Providers;

public class HmacSha256Provider : IEncryptionProvider
{
    public async Task<SaltAndHash> HashPasswordAsync(string password)
    {
        using var hmac = new HMACSHA256();

        var salt = hmac.Key;

        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var hash = await hmac.ComputeHashAsync(new MemoryStream(passwordBytes));

        return new SaltAndHash(salt, hash);
    }

    public async Task<bool> VerifyPasswordHash(string password, SaltAndHash saltAndHash)
    {
        using var hmac = new HMACSHA256(saltAndHash.Salt);
        
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var compute = await hmac.ComputeHashAsync(new MemoryStream(passwordBytes));

        return compute.SequenceEqual(saltAndHash.Hash);
    }
}