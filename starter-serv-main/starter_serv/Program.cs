using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using starter_serv.BusinessProviders;
using starter_serv.DataProviders;
using starter_serv.Helper;
using System.Text;
using AutoMapper;
using System;
using Serilog.Filters;
using starter_serv.CronJob;
using Cronos;
using starter_serv.Data;

var builder = WebApplication.CreateBuilder(args);

#region setup connection for sql server
// for sql server
//var connectionString = builder.Configuration.GetConnectionString("SQLServerConnection");
//builder.Services.AddDbContext<DbpmsbjbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddHttpContextAccessor();
#endregion

#region setup connection for postgres
// for postgres
var connectionString = builder.Configuration.GetConnectionString("PostgresServerConnection");
builder.Services.AddDbContext<DbMediaServicesContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddHttpContextAccessor();
#endregion

// Configuration LOG FILE
var logFilePath = $"Logs/LogFile-.txt";
builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    logging.ClearProviders();
    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

    logging.AddFile(logFilePath);
});

// Pass the configuration to the cron job service
//builder.Host.ConfigureServices((hostContext, services) =>
//{
//    services.Configure<CronJobOptions>(hostContext.Configuration.GetSection("CronJobOptions"));
//});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add Swagger generator
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "REST API STARTER-SERVICES", Version = "v1" });
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

//Add Jwt Token Functionality

// Add Authencation
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateIssuerSigningKey"]),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JsonWebTokenKeys:IssuerSigningKey"])),
        ValidateIssuer = true,
        ValidAudience = builder.Configuration["JsonWebTokenKeys:ValidAudience"],
        ValidIssuer = builder.Configuration["JsonWebTokenKeys:ValidIssuer"],
        ValidateAudience = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateAudience"]),
        RequireExpirationTime = bool.Parse(builder.Configuration["JsonWebTokenKeys:RequireExpirationTime"]),
        ValidateLifetime = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateLifetime"])
    };
});

builder.Services.AddCors(options =>
{
    //options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy(name: "Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

});

//builder.Services.AddHostedService<CronJobService>(); // Register CronJobService

builder.Services.AddAutoMapper(typeof(MappingProfile));

//GenericRepository
builder.Services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

// transient Data Provider
builder.Services.AddTransient<IUsersDataProvider, UsersDataProvider>();

// transient Business Provider
builder.Services.AddTransient<IAuthenticateBusinessProviders, AuthenticateBusinessProviders>();
builder.Services.AddTransient<IUsersBusinessProviders, UsersBusinessProviders>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAuthentication();

app.UseMiddleware<UnauthorizedMiddleware>();

app.UseCors();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

// start service cron job
//app.Map("/runcronjob", HandleCronJobRequest);

app.Run();


// handle service cron job
//async Task HandleCronJobRequest(HttpContext context)
//{
//    var serviceProvider = app.Services;
//    var cronJob = serviceProvider.GetRequiredService<CronJobService>();
//    await cronJob.ExecuteAsync();

//    await context.Response.WriteAsync("Cron job executed!");
//}
