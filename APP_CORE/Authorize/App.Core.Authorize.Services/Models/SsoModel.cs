using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorize.Services.Manager.Models
{
    public class SsoModel
    {
        [Required]
        public string UserName { get; set; }
        public string ClientId { get; set; }
        public string Token { get; set; }
        public string AppName { get; set; }
    }
}
