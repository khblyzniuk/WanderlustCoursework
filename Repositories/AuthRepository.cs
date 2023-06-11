using System.Security.Cryptography;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class AuthRepository : IAuthRepository
{        
    private readonly RepositoryDbContext _dbContext;
    public AuthRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

    public async Task<Auth> Register(UserForRegistrationDto userForRegistration, string passwordKey)
    {
        if (_dbContext.Users
            .Any(user => user != null && user.Email == userForRegistration.Email))
            throw new Exception("The user with this email already exists.");
        
        var passwordSalt = new byte[128 / 8];
        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(passwordSalt);
        
        var passwordHash = AuthHelper.GetPasswordHash(userForRegistration.Password,
            passwordSalt, passwordKey);

        var authEntity = new Auth
        {
            Email = userForRegistration.Email,
            PassHash = passwordHash,
            PassSalt = passwordSalt
        };

        await _dbContext.Auth.AddAsync(authEntity);

        var user = userForRegistration.Adapt<User>();
        user.CreatedAt = DateTime.Now;
        await _dbContext.Users.AddAsync(user);

        return authEntity;
    }

    public async Task<Guid> Login(UserForLoginDto userForLogin, string passwordKey)
    {
        var userForConfirmationEntity = await _dbContext.Auth
            .FirstOrDefaultAsync(auth => auth.Email == userForLogin.Email);

        
        if (userForConfirmationEntity == null)
        {
            throw new UserNotFoundException(userForLogin.Email);
        }

        var userForConfirmation = new UserForLoginConfirmationDto
        {
            PasswordHash = userForConfirmationEntity.PassHash,
            PasswordSalt = userForConfirmationEntity.PassSalt
        };

        var passwordHash = AuthHelper.GetPasswordHash(userForLogin.Password, userForConfirmation.PasswordSalt, passwordKey);

        if (passwordHash.Where((t, index) => t != userForConfirmation.PasswordHash[index]).Any())
        {
            throw new IncorrectPasswordException();
        }

        var loggedUser = await _dbContext.Users.FirstOrDefaultAsync(user => user != null && user.Email == userForLogin.Email);
        return loggedUser.Id;
    }
}