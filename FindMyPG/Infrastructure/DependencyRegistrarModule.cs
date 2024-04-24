using Autofac;
using FindMyPG.Core.Data;
using FindMyPG.Data;
using FindMyPG.Service.Authentication;
using FindMyPG.Service.Cities;
using FindMyPG.Service.JwtTokens;
using FindMyPG.Service.PGInfos;
using FindMyPG.Service.States;
using FindMyPG.Service.Users;
using FindMyPG.Service.ZipCodes;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace FindMyPG.Infrastructure
{
    public class DependencyRegistrarModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(context => new
            PGDBContext(context.Resolve<DbContextOptions<PGDBContext>>()))
            .As<IDbContext>();
            builder.Register(tokenHandler => new JwtSecurityTokenHandler());

            builder.RegisterGeneric(typeof(EFRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<PGInfoService>().As<IPgInfoService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StateService>().As<IStateService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CityService>().As<ICityService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ZipCodeService>().As<IZipCodeService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>()
               .InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>()
              .InstancePerLifetimeScope();
            builder.RegisterType<JwtTokenService>().As<IJwtTokenService>()
               .InstancePerLifetimeScope();
            builder.RegisterType<JwtTokenService>().As<IJwtTokenService>()
               .InstancePerLifetimeScope();
        }
    }
}
