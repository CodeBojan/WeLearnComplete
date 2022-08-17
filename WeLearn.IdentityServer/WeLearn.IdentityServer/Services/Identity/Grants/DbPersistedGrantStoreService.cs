using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Microsoft.EntityFrameworkCore;
using WeLearn.Data.Models.Identity;
using WeLearn.Data.Persistence;

namespace WeLearn.IdentityServer.Services.Identity.Grants;

public class DbPersistedGrantStoreService : IPersistedGrantStore
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _dbContext;

    public DbPersistedGrantStoreService(ILogger<DbPersistedGrantStoreService> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
    {
        var grants = await GetFilteredGrants(filter);
        return grants;
    }

    public async Task<PersistedGrant> GetAsync(string key)
    {
        var grant = await _dbContext.Grants.FindAsync(key);
        return grant;
    }

    public async Task RemoveAllAsync(PersistedGrantFilter filter)
    {
        var grants = await GetFilteredGrants(filter);

        if (!grants.Any())
            return;

        _dbContext.RemoveRange(grants);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(string key)
    {
        var grant = await _dbContext.Grants.FindAsync(key);
        if (grant is null)
            return;

        _dbContext.Remove(grant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task StoreAsync(PersistedGrant grant)
    {
        var existingGrant = await _dbContext.Grants.FindAsync(grant.Key);
        if (existingGrant is not null)
        {
            existingGrant.Update(grant);
            _dbContext.Entry(existingGrant).State = EntityState.Modified;
        }
        else
            _dbContext.Add(new DbPersistedGrant(grant));

        await _dbContext.SaveChangesAsync();
    }

    private Task<List<DbPersistedGrant>> GetFilteredGrants(PersistedGrantFilter filter)
    {
        return _dbContext.Grants.Where(g =>
                        filter.ClientId == g.ClientId
                        && filter.SessionId == g.SessionId
                        && filter.Type == g.Type
                        && filter.SubjectId == g.SubjectId)
                    .ToListAsync();
    }
}
