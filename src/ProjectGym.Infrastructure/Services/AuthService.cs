using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ProjectGym.Application.DTOs.Auth;
using ProjectGym.Application.DTOs.Common;
using ProjectGym.Application.Interfaces;
using ProjectGym.Domain.Entities;
using ProjectGym.Domain.Interfaces;
using ProjectGym.Infrastructure.Identity;

namespace ProjectGym.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<AppIdentityUser> userManager, IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public Task<Result<UserDto>> GetProfileAsync(string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<LoginResponseDto>> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<UserDto>> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if(existingUser is not null) 
            return Result<UserDto>.Conflict("Bu email adresi zaten kayıtlı.");
        var identityUser = new AppIdentityUser
        {
            FirstName= dto.FirstName,
            LastName=dto.LastName,
            Email=dto.Email,
            UserName=dto.Email,
            CreatedAt=DateTime.UtcNow
        };
        var result = await _userManager.CreateAsync(identityUser, dto.Password);
        if(!result.Succeeded)
        {
            var errors = result.Errors.Select(e=>e.Description);
            return Result<UserDto>.ValidationFailure(string.Join(", ",errors));
        }
        await _userManager.AddToRoleAsync(identityUser, "Member");
        var member = new Member
        {
            UserId=identityUser.Id,
            FirstName=dto.FirstName,
            LastName=dto.LastName,
            Email=dto.Email,
            JoinDate=DateTime.UtcNow,
            IsActive=true
        };
        await _unitOfWork.Members.AddAsync(member, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var roles = await _userManager.GetRolesAsync(identityUser);
        // dönüş yaparken, UserDto'ya map ederken kullnacağız
        return null;
    }
}
