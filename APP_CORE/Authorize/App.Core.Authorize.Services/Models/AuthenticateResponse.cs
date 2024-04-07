﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Authorize.Services.Services.Models
{
    public class AuthenticateResponse : BaseAuthenticateResponse
    {
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public int? UserType { get; set; }
        public string Image { get; set; }
        public string Provider { get; set; }

        //[JsonIgnore] // refresh token is returned in http only cookie

        public AuthenticateResponse(UserModel user, string jwtToken, string refreshToken, string provider= "") : base(user, jwtToken, refreshToken)
        {
            Id = user.Id;
            UnitCode = user.UnitCode;
            UnitName = user.UnitName;
            Provider = provider;
            UserType = user.UserType;
        }
    }

    public class BaseAuthenticateResponse
    {
        public BaseAuthenticateResponse(UserModel user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
            FullName = user.FullName;
            Username = user.Username;
            name = user.FullName;//new core
            email = user.email;//new core
            avatar = user.avatar;//new core
            status = user.status;//new core
            UserType = user.UserType;
            UnitCode = user.UnitCode;
            UnitName = user.UnitName;
            Tag = user.Tag;
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string name { get; set; }
        public string Username { get; set; }
        public string email { get; set; }//new core
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public int? UserType { get; set; }
        public string avatar { get; set; }//new core 'assets/images/avatars/brian-hughes.jpg',
        public string status { get; set; }//new core
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string Tag { get; set; }
    }
}
