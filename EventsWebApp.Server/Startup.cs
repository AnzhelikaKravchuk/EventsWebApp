using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Mapper;
using EventsWebApp.Infrastructure.Handlers;
using EventsWebApp.Infrastructure.Repositories;
using EventsWebApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using EventsWebApp.Infrastructure.UnitOfWork;
using EventsWebApp.Server.Extensions;
using EventsWebApp.Server.ExceptionsHandler;
using AutoMapper;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using EventsWebApp.Infrastructure.DataSeeder;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Application.Extensions;
using EventsWebApp.Application.Services.ImageService;
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
            string connection = Configuration["SqlConnectionString"] ?? throw new Exception("No database connection string");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => {
                    policy.WithOrigins(Configuration["ClientUrl"]);
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                    });
            });
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
            services.AddTransient<DataSeeder>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAttendeeRepository, AttendeeRepository>();
            services.AddScoped<ISocialEventRepository, SocialEventRepository>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.AddApiAuthentication(Configuration);

            services.AddMediatRServices();
            services.AddControllers().AddJsonOptions(x =>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(new AttendeeDtoMappingProfile()));

            services.AddAutoMapper(typeof(AttendeeDtoMappingProfile));
            services.AddAutoMapper(typeof(AddUpdateAttendeeRequestMappingProfile));
            services.AddAutoMapper(typeof(CreateSocialEventCommandMappingProfile));
            services.AddAutoMapper(typeof(RegisterUserCommandMappingProfile));
            services.AddAutoMapper(typeof(SocialEventDtoMappingProfile));
            services.AddAutoMapper(typeof(UpdateSocialEventCommandMappingProfile));
            services.AddAutoMapper(typeof(UpdateUserCommandMappingProfile));
            services.AddAutoMapper(typeof(UserDtoMappingProfile));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env, string[] args)
        {
            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                SeedData(app);
            }

            app.MigrateDatabase();
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
                RequestPath = "/images",
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=1800");
                }
            });

            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<StatusCodeInterceptorMiddleware>();
            app.UseExceptionHandler();
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();

        }

        private void SeedData(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory?.CreateScope())
            {
                var service = scope?.ServiceProvider.GetService<DataSeeder>();
                service?.Seed();
            }
        }

    }
}