using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using service;

namespace APIGateway.Controllers;

[Route("[Controller]")]
public class AuthenticationController :GrpcControllerBase<AuthenticationActivity.AuthenticationActivityClient>
{
    [HttpGet]
    [Route("SignIn/Attempt")]
    public async Task<IActionResult> SignIn(string username, string password)
    {
        var response = await Service.SignInAsync( new(){Password = password,Username = username});
        return Ok(response);
    }

    [HttpPost]
    [Route("SignUp/Create")]
    public async Task<IActionResult> SignUp(string username, string password, string email)
    {
        var response = await Service.SignUpAsync(new(){ Email = email, Password = password, Username = username });
        return Ok(response);
    }

    [HttpPut]
    [Route("ChangePassword")]
    public async Task<IActionResult> ChangePassword(string username, string password)
    {
        var response = await Service.ChangePasswordAsync(new (){ NewPassword = password, Username = username });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await Service.GetByIdAsync(new StringValue() { Value = id.ToString() });
        return Ok(response.Email);
    }
}