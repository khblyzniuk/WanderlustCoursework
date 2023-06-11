using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class RouteRepository : IRouteRepository
{
    private readonly RepositoryDbContext _dbContext;

    public RouteRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<Route>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await _dbContext.Routes.Where(x => x .UserId == userId).ToListAsync(cancellationToken);

    public async Task<Route> GetByIdAsync(Guid routeId, CancellationToken cancellationToken = default) =>
        await _dbContext.Routes.FirstOrDefaultAsync(x => x.Id == routeId, cancellationToken);

    public void Insert(Route route) => _dbContext.Routes.Add(route);

    public void Remove(Route route) => _dbContext.Routes.Remove(route);
}