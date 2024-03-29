﻿using System.ComponentModel.DataAnnotations;

namespace TodoManager.Common.Models.Users;

public class AuthenticateRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    public AuthenticateRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}
