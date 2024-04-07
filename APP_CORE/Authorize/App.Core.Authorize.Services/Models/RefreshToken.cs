using App.Qtht.Dal.EntityClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Authorize.Services.Services.Models
{
    //[Owned]
    //public class RefreshToken
    //{
    //    [Key]
    //    [JsonIgnore]
    //    public int Id { get; set; }

    //    public string Token { get; set; }
    //    public DateTime Expires { get; set; }
    //    public bool IsExpired => DateTime.UtcNow >= Expires;
    //    public DateTime Created { get; set; }
    //    public string CreatedByIp { get; set; }
    //    public DateTime? Revoked { get; set; }
    //    public string RevokedByIp { get; set; }
    //    public string ReplacedByToken { get; set; }
    //    public bool IsActive => Revoked == null && !IsExpired;
    //}

    public class TokenForRefresh
    {
        public string Tag { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshToken : SysRefreshtokenEntity
    {
        public string GenerateToken { get; set; }
        public SysRefreshtokenEntity GetIntanceEntity()
        {
            return this;
        }
    }
}