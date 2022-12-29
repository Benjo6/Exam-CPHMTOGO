using AuthenticationService.Application.Contracts.Commands;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using service;

namespace AuthenticationService;

public class ServiceProfile:Profile
{
    public ServiceProfile()
    {
        CreateMap<SignInRequest, SignInCommand>();
        CreateMap<SignUpRequest, SignUpCommand>();
        CreateMap<ChangePasswordRequest, ChangePasswordCommand>();


    }
}