using Mifaz.ApiModels;
using Mifaz.Database;
using Mifaz.Models;

namespace Mifaz.Services;

public interface IUserService
{
    Task<AuthUserResponse> Authenticate(AuthUserRequest request, CancellationToken token);
    Task<IEnumerable<User>> GetAll(CancellationToken token);
    Task<CreateUserResponse> CreateUser(CreateUserRequest request,
        CancellationToken token);
    Task<ResetPasswordResponse> Reset(ResetPasswordRequest request, CancellationToken token);
}

public class UserService : IUserService
{
    private readonly MifazDbContext _context;

    public UserService(MifazDbContext context)
    {
        _context = context;
    }

    public async Task<AuthUserResponse> Authenticate(AuthUserRequest request, CancellationToken token)
    {
        var user = await _context.GetUser(request.Mail, request.Password, token);

        // on auth fail: null is returned because user is not found
        // on auth success: user object is returned
        return new AuthUserResponse { User = user };
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken token)
    {
        return await _context.GetUsers(token);
    }

    public async Task<CreateUserResponse> CreateUser(CreateUserRequest request,
        CancellationToken token)
    {
        var user = await _context.CreateUser(request.Mail, request.Password, request.FirstName, request.LastName,
            request.Phone, token);
        return new CreateUserResponse { User = user };
    }

    public async Task<ResetPasswordResponse> Reset(ResetPasswordRequest request, CancellationToken token)
    {
        var result = await _context.ResetPassword(request.Mail, token);
        return new ResetPasswordResponse { Result = result };
    }
}