namespace Mifaz.ApiModels;

public class CreateUserRequest
{
    public string Mail { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
}