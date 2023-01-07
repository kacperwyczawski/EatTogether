﻿using EatTogether.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace EatTogether.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Email, 
    string Password, 
    string FirstName, 
    string LastName) : IRequest<ErrorOr<AuthenticationResult>>;