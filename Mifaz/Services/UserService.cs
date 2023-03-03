using Mifaz.Database;
using Mifaz.Models;

namespace Mifaz.Services;

public interface IUserService
{
    Task<User> CreateUser(string mail, string password, string firstName, string lastName, string phone, CancellationToken token);
    Task<User> Authenticate(string mail, string password, CancellationToken token);
    Task<IEnumerable<User>> GetAll(CancellationToken token);
}

public class UserService : IUserService
{
    private readonly MifazDbContext _context;

    public UserService(MifazDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUser(string mail, string password, string firstName, string lastName, string phone, CancellationToken token)
    {
        return await _context.CreateUser(mail, password, firstName, lastName, phone, token);
    }

    public async Task<User> Authenticate(string mail, string password, CancellationToken token)
    {
        var user = await _context.GetUser(mail, password, token);

        // on auth fail: null is returned because user is not found
        // on auth success: user object is returned
        return user;
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken token)
    {
        return await _context.GetUsers(token);
    }
}