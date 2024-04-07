using System.ComponentModel.DataAnnotations;

namespace App.Qtht.Services.Authorize;

public class AuthenticateRequest
{
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }
    public string DinhDanhApp { get; set; }
    public int? AppId { get;set; }
    public string Tag { get; set; }
    public string checkDevice { get; set; }
}