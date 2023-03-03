using System.ComponentModel.DataAnnotations;

namespace Mifaz.Models;

public class User
{
    [Key] public int Id { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
}