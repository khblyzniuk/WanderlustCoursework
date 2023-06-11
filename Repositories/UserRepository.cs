using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public UserRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<User?>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbContext.Users.ToListAsync(cancellationToken);

        public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
            await _dbContext.Users.FirstOrDefaultAsync(x => x != null && x.Id == userId, cancellationToken);

        public void Insert(User? user) => _dbContext.Users.Add(user);

        public void Remove(User? user) => _dbContext.Users.Remove(user);
    }
}
