using System.ComponentModel.DataAnnotations;

namespace App.Qtht.Services.Authorize;

public class SsoModel
{
    [Required] public string UserName { get; set; }

    public string ClientId { get; set; }
    public string Token { get; set; }
    public string AppName { get; set; }
}