using Mifaz.Database;
using Mifaz.Models;

namespace Mifaz.Services;

public interface IUserService {
    Task<User> CreateUser(string username, string password, CancellationToken token);
    Task<User> Authenticate(string username, string password, CancellationToken token);
    Task<IEnumerable<User>> GetAll(CancellationToken token);
}
public class UserService : IUserService {
    private readonly MifazDbContext _context;
    private readonly PasswordHashingService _passwordHashingService;

    public UserService(MifazDbContext context, PasswordHashingService passwordHashingService) {
        _context = context;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<User> CreateUser(string username, string password, CancellationToken token) {
        return await _context.CreateUser(username, password, token);
    }

    public async Task<User> Authenticate(string username, string password, CancellationToken token) {
        var user = await _context.GetUser(username, password, token);

        // on auth fail: null is returned because user is not found
        // on auth success: user object is returned
        return user;
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken token) {
        return await _context.GetUsers(token);
    }
}