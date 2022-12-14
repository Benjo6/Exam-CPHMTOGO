using AuthenticationService.Application;
using AuthenticationService.Application.Contracts.Commands;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using service;
using static service.AuthenticationActivity;

namespace AuthenticationService.Service;

public class AuthenticationService : AuthenticationActivityBase
{
    private readonly IAuthenticationApplicationRepository _repository;
    private readonly IMapper _mapper;

    public AuthenticationService(IAuthenticationApplicationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override async Task<BoolValue> SignIn(SignInRequest request, ServerCallContext context)
    {
        var response = await _repository.SignIn(_mapper.Map<SignInCommand>(request));
        return new BoolValue
        {
            Value = response
        };
    }
    public override async Task<BoolValue> SignUp(SignUpRequest request, ServerCallContext context)
    {
        var response = await _repository.SignUp(_mapper.Map<SignUpCommand>(request));
        return new BoolValue
        {
            Value = response
        };
    }
    
    public override async Task<BoolValue> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
    {
        var response = await _repository.ChangePassword(_mapper.Map<ChangePasswordCommand>(request));
        return new BoolValue
        {
            Value = response
        };
    }

    public override async Task<LoginInfoResponse> GetById(StringValue request, ServerCallContext context)
    {
        var response = await _repository.GetById(new Guid(request.Value));
        return new LoginInfoResponse()
            { Email = response.Email, Id = response.Id.ToString(), Username = response.Username };
    }
}