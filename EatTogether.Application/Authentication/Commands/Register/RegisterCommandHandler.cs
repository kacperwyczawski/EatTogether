﻿using EatTogether.Application.Authentication.Common;
using EatTogether.Application.Common.Interfaces.Authentication;
using EatTogether.Application.Common.Interfaces.Persistence;
using EatTogether.Domain.Errors;
using EatTogether.Domain.Features.UserAggregate;
using ErrorOr;
using MediatR;

namespace EatTogether.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetByEmail(command.Email) is not null)
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.User.DuplicateEmail);

        var user = new User(Guid.NewGuid(), command.FirstName, command.LastName, command.Email, command.Password,
            DateTime.Now, DateTime.Now);

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return Task.FromResult<ErrorOr<AuthenticationResult>>(new AuthenticationResult(user, token));
    }
}