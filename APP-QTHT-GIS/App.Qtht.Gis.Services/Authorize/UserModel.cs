using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace App.Qtht.Services.Authorize;

public class UserModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string name { get; set; } //new core
    public string Username { get; set; }
    public string maTinh { get; set; }
    public string maHuyen { get; set; }
    public string maXa { get; set; }
    public string email { get; set; } //new core
    public string phone { get; set; }
    public string diachi { get; set; }
    public string avatar { get; set; } //new core 'assets/images/avatars/brian-hughes.jpg',
    public string status { get; set; } //new core
    public decimal? AppId { get; set; }
    public string UnitCode { get; set; }
    public string UnitName { get; set; }
    public string Tag { get; set; }
    public int? UserType { get; set; }

    [JsonIgnore] public string Token { get; set; }

    [JsonIgnore] public List<MenuOfUserModel> lstMenu { get; set; }

    [JsonIgnore] public string Password { get; set; }

    [JsonIgnore] public List<RefreshToken> RefreshTokens { get; set; }

    //id    : 'cfaad35d-07a3-4447-a6c3-d8c3d54fd5df',
    //name  : 'Brian Hughes',
    //email : 'hughes.brian@company.com',
    //avatar: 'assets/images/avatars/brian-hughes.jpg',
    //status: 'online'
}

public class MenuOfUserModel
{
    public string State { get; set; }
    public string Function { get; set; }
}