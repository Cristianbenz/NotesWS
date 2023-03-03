using DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotesApi.Models;
using NotesApi.Services;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

//Cors
string angularCors = "AngularNoteClient";
builder.Services.AddCors( config =>
{
    config.AddPolicy(
        name: angularCors,
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        }
        );
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotesService, NotesService>();

// Add dbContext
builder.Services.AddDbContext<NotesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"))
);

// JWT configuration
IConfigurationSection jwtConfigSection = builder.Configuration.GetSection("JwtConfig");
builder.Services.Configure<JwtConfiguration>(jwtConfigSection);
JwtConfiguration jwtConfig = jwtConfigSection.Get<JwtConfiguration>();
string key = jwtConfig.Secret;

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(config =>
    {
        config.SaveToken = true;
        config.RequireHttpsMetadata = true;
        config.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime= true,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(angularCors);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
