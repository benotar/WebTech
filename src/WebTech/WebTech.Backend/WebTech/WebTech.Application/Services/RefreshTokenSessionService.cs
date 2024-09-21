using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WebTech.Application.Common;
using WebTech.Application.Configurations;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Cache;
using WebTech.Domain.Enums;

namespace WebTech.Application.Services;

public class RefreshTokenSessionService : IRefreshTokenSessionService
{
    private readonly IDistributedCache _redis;

    private readonly RefreshTokenSessionConfiguration _refreshTokenSessionConfiguration;

    private readonly IDateTimeProvider _dateTimeProvider;

    public RefreshTokenSessionService(IDistributedCache redis, RefreshTokenSessionConfiguration refreshTokenSessionConfiguration, IDateTimeProvider dateTimeProvider)
    {
        _redis = redis;
        
        _refreshTokenSessionConfiguration = refreshTokenSessionConfiguration;
        
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<None>> CreateOrUpdateAsync(Guid userId, string fingerprint, string refreshToken)
    {
        var redisKey = RefreshSession.GetCacheKey(userId, fingerprint);

        var entity = new RefreshSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Fingerprint = fingerprint,
            RefreshToken = refreshToken,
            ExpiryAt = _dateTimeProvider.UtcNow.AddDays(_refreshTokenSessionConfiguration.ExpirationDays)
        };

        var redisValue = JsonSerializer.Serialize(entity);

        await _redis.SetStringAsync(redisKey, redisValue,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_refreshTokenSessionConfiguration.ExpirationDays)
            });

        return Result<None>.Success();
    }

    public async Task<Result<None>> DeleteAsync(Guid userId, string fingerprint)
    {
        var redisKey = RefreshSession.GetCacheKey(userId, fingerprint);

        await _redis.RemoveAsync(redisKey);
        
        return Result<None>.Success();

    }

    public async Task<Result<bool>> IsSessionKeyExistsAsync(Guid userId, string fingerprint)
    {
        var redisKey = RefreshSession.GetCacheKey(userId, fingerprint);

        return await _redis.GetStringAsync(redisKey) is not null
            ? Result<bool>.Success()
            : Result<bool>.Error(ErrorCode.SessionNotFound);
    }
}