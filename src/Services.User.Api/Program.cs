using System.Collections.Generic;
using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.Secrets.Vault;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services.User.Application;
using Services.User.Application.Commands;
using Services.User.Application.DTO;
using Services.User.Application.Queries;
using Services.User.Infrastructure;

namespace Services.User.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await CreateWebHostBuilder(args)
                .Build()
                .RunAsync();
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                        .Get<GetUsers, IEnumerable<UserDto>>("users")
                        .Get<GetUser, UserDetailsDto>("users/{userId}")
                        .Get<GetUserState, UserStateDto>("users/{userId}/state")
                        .Post<CompleteUserRegistration>("users",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"users/{cmd.UserId}"))
                        .Put<ChangeUserState>("users/{userId}/state/{state}",
                            afterDispatch: (cmd, ctx) => ctx.Response.NoContent())))
                .UseLogging()
                .UseVault();
    }
}