using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class JointRepository : IJointRepository
{
    private readonly RepositoryDbContext _dbContext;
    public JointRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<IEnumerable<Joint>> GetAllByRouteIdAsync(Guid routeId, CancellationToken cancellationToken = default) =>
        await _dbContext.Joints.Where(x => x.RouteId == routeId).ToListAsync(cancellationToken);

    public async Task<Joint> GetByIdAsync(Guid jointId, CancellationToken cancellationToken = default)
        => await _dbContext.Joints.FirstOrDefaultAsync(x => x.Id == jointId, cancellationToken);
    
    public void Insert(Joint joint) => _dbContext.Joints.Add(joint);

    public void Remove(Joint joint) => _dbContext.Joints.Remove(joint);
}