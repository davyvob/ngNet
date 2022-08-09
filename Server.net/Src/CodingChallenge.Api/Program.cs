using CodingChallenge.Core.Entities;
using CodingChallenge.Core.Infrastructure;
using CodingChallenge.Core.Managers;
using CodingChallenge.Core.Managers.Interfaces;
using CodingChallenge.Core.Services;
using CodingChallenge.Core.Services.Interfaces;
using CodingChallenge.Core.Validators;
using CodingChallenge.Infrastructure.Data;
using CodingChallenge.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.


//override modelstatefilter
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers();

builder.Services.AddDbContext<CodingChallengeDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TestCodeChallengeDb")));



//identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<CodingChallengeDbContext>();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwtOptions =>
    {
        jwtOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = Configuration["JWT:Issuer"],
            ValidAudience = Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SigningKey"]))
        };
    });



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Imi.Project.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                        new OpenApiSecurityScheme
                        {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                        },
                        new string[] {}
                  }
                });
            });


builder.Services.AddHttpContextAccessor();


//register di services and interfaces
builder.Services.AddScoped<ICodeChallengeRepository, CodeChallengeRepository>();
builder.Services.AddScoped<ICodingChallengeService, CodingChallengeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGameManager, GameManager>();
builder.Services.AddScoped<IPlayerStatsRepository, PlayerStatsRepository>();
builder.Services.AddScoped<IUserCodingChallengeRepository, CompletedCodeChallengeRepository>();
builder.Services.AddScoped<IValidatorService, ValidatorService>();

//builder.Services.AddCors(options => options.AddPolicy(name: "CodeChallengeContext", 
//    policy =>
//    {
//        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
//    }));

builder.Services.AddCors(options => options.AddPolicy(name: "CodeChallengeContext",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
        policy.WithOrigins("http://192.168.0.8:4200").AllowAnyMethod().AllowAnyHeader();
    }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
     //   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imi.Project.Api v1");
     //   c.RoutePrefix = string.Empty;

    });
}

app.UseCors("CodeChallengeContext");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
