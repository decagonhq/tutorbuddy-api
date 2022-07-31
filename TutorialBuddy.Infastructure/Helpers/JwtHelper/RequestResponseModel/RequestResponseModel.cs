using System.ComponentModel.DataAnnotations;
using TutorBuddy.Core.Models;

public class RequestModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public class ResponseModel
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

    public ResponseModel(User user, string token)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Token = token;
    }
}