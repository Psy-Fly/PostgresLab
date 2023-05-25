using System.Security.Claims;
using PostgresLab.ViewModels;

namespace PostgresLab.Services.Interfaces;

public interface IAccountService
{
    Task<BaseResponce.BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
    Task<BaseResponce.BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
}