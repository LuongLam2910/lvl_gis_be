﻿namespace App.Qtht.Services.Authorize;

public class RegisterRequest
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string AppId { get; set; }
}