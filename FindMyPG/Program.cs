using Autofac;
using Autofac.Extensions.DependencyInjection;
using FindMyPG.Core.Configs;
using FindMyPG.Core.Entities;
using FindMyPG.Data;
using FindMyPG.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FindMyPG.Infrastructure.Validators;
using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using FindMyPG.Middlewares;
using Swashbuckle.AspNetCore.SwaggerGen.ConventionalRouting;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.ConfigureKestrel((context, serverOptions) =>
//{
//    var kestrelSection = context.Configuration.GetSection("Kestrel");

//    serverOptions.Configure(kestrelSection)
//        .Endpoint("HTTPS", listenOptions =>
//        {

//        });
//});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new DependencyRegistrarModule());
    });
// Add services to the container.
var config = builder.Configuration.GetSection("FindMyPGConfig").Get<FindMyPGConfig>();
builder.Services.Configure<FindMyPGConfig>(
    builder.Configuration.GetSection("FindMyPGConfig"));

builder.Services.AddDbContext<PGDBContext>(options =>
options.UseLazyLoadingProxies().UseSqlServer(config.SqlConnectionsString));

builder.Services.AddAutoMapper(typeof(Program));

var mvcBuilder = builder.Services.AddControllers();
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password = new PasswordOptions()
    {
        RequireDigit = false,
        RequiredLength = 8,
        RequiredUniqueChars = 0,
        RequireLowercase = true,
        RequireUppercase = true,
        RequireNonAlphanumeric = true
    };
}).AddEntityFrameworkStores<PGDBContext>()
.AddDefaultTokenProviders();
//.AddPasswordValidator<PGPasswordValidator<User>>();

mvcBuilder.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwt =>
                {
                    jwt.RequireHttpsMetadata = false;
                    jwt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtSigningKey)),
                        ValidateIssuerSigningKey = true,
                        ValidAudience = config.JwtValidAudience,
                        ValidateAudience = true,
                        ValidIssuer = config.JwtValidIssuer,
                        ValidateIssuer = true,
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true
                    };

                    jwt.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            //Set custom header on token expiration
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Dummy API", Version = "v1" });
    }
    );
builder.Services.AddSwaggerGenWithConventionalRoutes(options =>
{
    options.IgnoreTemplateFunc = (template) => template.StartsWith("api/");
    options.SkipDefaults = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}");
    ConventionalRoutingSwaggerGen.UseRoutes(endpoints);
});
//app.MapControllerRoute(
//    name: "",
//    pattern: "{controller}/{action}/{id?}");
app.UseHttpsRedirection();
app.UseMiddleware<ApiResponseMiddleware>();
app.MapControllers();

app.Run();
