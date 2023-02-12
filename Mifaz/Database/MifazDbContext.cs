using System.Data;
using Microsoft.EntityFrameworkCore;
using Mifaz.Models;
using Mifaz.Services;

namespace Mifaz.Database;

public class MifazDbContext : DbContext {
    private readonly PasswordHashingService _passwordHashingService;
    public virtual DbSet<User> Users { get; set; }

    public MifazDbContext(DbContextOptions<MifazDbContext> options, PasswordHashingService passwordHashingService) : base(options) {
        _passwordHashingService = passwordHashingService;
    }

    public async Task<User> CreateUser(string username, string password, CancellationToken token) {
        if (await Users.FirstOrDefaultAsync(x => x.Username == username, token) is not null)
            throw new ConstraintException("User already exists");
        var hashedPassword = _passwordHashingService.Hash(password);
        var user = new User {Username = username, Password = hashedPassword};
        Users.Add(user);
        await SaveChangesAsync(token);
        return user;
    }

    public async Task<User> GetUser(string username, string password, CancellationToken token) {
        var user = await Users.FirstOrDefaultAsync(x => x.Username == username, token);
        if (_passwordHashingService.Verify(password, user!.Password))
            return user;
        throw new UnauthorizedAccessException();
    }

    public async Task<IEnumerable<User>> GetUsers(CancellationToken token) {
        return await Users.ToListAsync(token);
    }
}