using System.Security.Cryptography;
using System.Text;
using AuthenticationService.Application.Contracts.Commands;
using AuthenticationService.Domain;
using AuthenticationService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Application;

public class AuthenticationApplicationRepository : IAuthenticationApplicationRepository
{
    private readonly AuthenticationDbContext _dbContext;

    public AuthenticationApplicationRepository(AuthenticationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> SignIn(SignInCommand request)
    {
        var login = await _dbContext.Infos.FirstOrDefaultAsync(x => x.Username == request.Username);
        if (login == null)
            return false; //User does not exist
        if (!VerifyPassword(request.Password, login.PasswordHash, login.PasswordSalt))
            return false;
        return true;
    }

    public async Task<bool> SignUp(SignUpCommand request)
    {
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(request.Password,out passwordHash,out passwordSalt);
        var signup = new LoginInfo()
        {
            Email = request.Email,
            Username = request.Username,
            PasswordSalt = passwordSalt,
            PasswordHash = passwordHash
        };
        if (UserExists(request.Username).Result)
        {
            throw new Exception("Username already exists");
        }
        await _dbContext.Infos.AddAsync(signup);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangePassword(ChangePasswordCommand request)
    {
        byte[] passwordHash, passwordSalt;
        if (!UserExists(request.Username).Result)
        {
            return false;
        }

        var result = await _dbContext.Infos.SingleOrDefaultAsync(p => p.Username == request.Username);
        if (result!=null)
        {
            CreatePasswordHash(request.NewPassword,out passwordHash,out passwordSalt);
            result.PasswordSalt = passwordSalt;
            result.PasswordHash = passwordHash;
            await _dbContext.SaveChangesAsync();

            return true;
        }
        return false;
    }

    private async Task<bool> UserExists(string username)
    {
        if (await _dbContext.Infos.AnyAsync(x=>x.Username==username))
        {
            return true;
        }

        return false;
    }
    

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i]) return false;
            }
        }
        return true;
    }
}