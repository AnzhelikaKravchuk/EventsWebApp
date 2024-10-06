using EventsWebApp.Application.Interfaces;
using EventsWebApp.Server.Mapper;
using EventsWebApp.Application.Services;
using EventsWebApp.Infrastructure.Handlers;
using EventsWebApp.Infrastructure.Repositories;
using EventsWebApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using EventsWebApp.Infrastructure.UnitOfWork;
using EventsWebApp.Application.Validators;
using EventsWebApp.Server.Extensions;
using EventsWebApp.Server.ExceptionsHandler;
using AutoMapper;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;

namespace EventsWebApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("No database connection string");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => {
                    policy.WithOrigins("https://localhost:5173");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                    });
            });
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            //services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAttendeeRepository, AttendeeRepository>();
            services.AddScoped<ISocialEventRepository, SocialEventRepository>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<UserValidator>();
            services.AddScoped<SocialEventValidator>();
            services.AddScoped<AttendeeValidator>();

            services.AddScoped<UserService>();
            services.AddScoped<SocialEventService>();
            services.AddScoped<AttendeeService>();
            services.AddScoped<ImageService>();

            services.AddApiAuthentication(Configuration);

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve); ; ;
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppMappingProfile());
            });

            services.AddAutoMapper(typeof(AppMappingProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always, 
            });

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")),
                RequestPath = "/images"
            });

            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseExceptionHandler();

        }
        
    }
}