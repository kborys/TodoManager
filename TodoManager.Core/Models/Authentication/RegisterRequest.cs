﻿using System.ComponentModel.DataAnnotations;

namespace TodoManager.Application.Models.Authentication;

public class RegisterRequest
{
    [Required]
    [MinLength(6)]
    [MaxLength(32)]
    public string UserName { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }

    public RegisterRequest(string userName, string firstName, string lastName, string password, string emailAddress)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        EmailAddress = emailAddress;
    }
}
