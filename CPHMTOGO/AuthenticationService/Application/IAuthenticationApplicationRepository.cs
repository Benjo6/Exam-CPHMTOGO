// <copyright file="IAuthenticationApplicationRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AuthenticationService.Application;

using AuthenticationService.Application.Contracts.Commands;
using AuthenticationService.Domain;

public interface IAuthenticationApplicationRepository
{
    Task<bool> SignIn(SignInCommand request);

    Task<bool> SignUp(SignUpCommand request);

    Task<bool> ChangePassword(ChangePasswordCommand request);

    Task<LoginInfo> GetById(Guid id);
}