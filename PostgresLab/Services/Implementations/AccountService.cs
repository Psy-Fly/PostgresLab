using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresLab.Helpers;
using PostgresLab.Repositories.Interfaces;
using PostgresLab.Services;
using PostgresLab.Services.Enums;
using PostgresLab.Services.Interfaces;
using PostgresLab.ViewModels;

namespace PostgresLab.Services.Implementations;

public class AccountService : IAccountService
{
    private IWorkerRepository workerRepository;
    private IUserRepository userRepository;
    private IClientContactsRepository clientContactsRepository;
    private HashPasswordHelper passwordHelper;
    private AcmeDataContext context;
    private IConfiguration configuration;
    private ConnectionSingleton connectionSingleton;

    public AccountService(IWorkerRepository workerRepository, AcmeDataContext context, IUserRepository userRepository, IConfiguration configuration, ConnectionSingleton connectionSingleton, IClientContactsRepository clientContactsRepository)
    {
        this.workerRepository = workerRepository;
        this.context = context;
        this.userRepository = userRepository;
        this.configuration = configuration;
        this.connectionSingleton = connectionSingleton;
        this.clientContactsRepository = clientContactsRepository;
        passwordHelper = new HashPasswordHelper();
    }

    public async Task<BaseResponce.BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
    {
        try
        {
            var user = userRepository.GetUserByLogin(model.UserLogin);

            if (user != null)
            {
                return new BaseResponce.BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь с таким логином уже существует"
                };
            }

            var cont = context.WorkerContacts.Add(model.Contacts);
            await context.SaveChangesAsync();
            

            var worker = new Worker()
            {
                WorkerLogin = model.UserLogin,
                Age = model.Age,
                Experience = model.Experience,
                Fullname = model.Fullname,
                Funciton = model.UserRole,
                OrganizationId = model.OrganizationId,
                ContactsId = cont.Property(x => x.Id).CurrentValue
            };

            var newUser = new UserAccount()
            {
                UserLogin = model.UserLogin,
                UserRole = model.UserRole,
                UserPassword = passwordHelper.HashPassword(model.UserPassword),
                Worker = worker
            };
            
            await workerRepository.CreateWorker(worker);
            await userRepository.CreateUser(newUser, model.UserPassword);
            
            var result = Authenticate(newUser);

            return new BaseResponce.BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Объект добавлен",
                StatusCode = StatusCode.OK

            };
        }
        catch (Exception ex)
        {

            return new BaseResponce.BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    
    [HttpPost]
    public async Task<BaseResponce.BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
    {
        try
        {
            var user = userRepository.GetUserByLogin(model.Login);

            if (user == null)
            {
                return new BaseResponce.BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователя с таким логином не существует!"
                };
            }

            if (passwordHelper.HashPassword(model.Password) != user.UserPassword)
            {
                return new BaseResponce.BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверный пароль!"
                };
            }
            
            connectionSingleton.ChangeConnectionUser(model.Login, model.Password);
            
            var result = Authenticate(user);
            return new BaseResponce.BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK

            };
        }
        catch (Exception ex)
        {

            return new BaseResponce.BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    private ClaimsIdentity Authenticate(UserAccount user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserLogin),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserRole)
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}