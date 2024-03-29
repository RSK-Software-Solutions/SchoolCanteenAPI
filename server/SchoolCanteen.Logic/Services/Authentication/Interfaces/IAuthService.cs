﻿
using SchoolCanteen.DATA.Models.Auth;

namespace SchoolCanteen.Logic.Services.Authentication.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string email, string username, string password, string role, string companyName);
    Task<AuthResult> LoginAsync(string email, string password);
}
