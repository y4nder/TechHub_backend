﻿using System.Net;
using domain.entities;
using MediatR;

namespace application.useCases.authentications.RegisterUser;

public class RegisterUserCommand : IRequest<RegisterUserResponse>
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class RegisterUserResponse
{
    public String Message { get; set; } = String.Empty;
    public UserMinimalDto CreatedUser { get; set; } = null!;
    public string Token { get; set; } = null!;
}