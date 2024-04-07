using System.ComponentModel.DataAnnotations;

namespace Authorize.Services.Services.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int? AppId { get; set; }
        public string DinhDanhApp { get; set; }
        public string Tag { get; set; }
    }
}