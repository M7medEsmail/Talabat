using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.IRepositories;
using Talabat.Domain.Services;
using Talabat.Extensions;
using Talabat.Helpers;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Identity;
using Talabat.Repository.Identity;
using Talabat.Service;

var builder = WebApplication.CreateBuilder(args);


#region Configure Service

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TalabatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// allow Dependancy injection to AppDbContext
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddTransient<EmailService>();


builder.Services.AddSingleton<IConnectionMultiplexer>(s =>
{
    var Connection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(Connection);
}); // used singlton to still open redis when user in site

var configuration = builder.Configuration;
builder.Services.AddIdentityService(configuration);
builder.Services.AddApplicationService(); 
#endregion

var app = builder.Build();

#region Apply Migration and Data Seeding Automatic during Run app    
var scope = app.Services.CreateScope();
var service =  scope.ServiceProvider;
 var loggerFactory = service.GetRequiredService<ILoggerFactory>();
 try
{
    var context = service.GetRequiredService<TalabatContext>();
    await context.Database.MigrateAsync();  // update database

    var IdentityContext = service.GetRequiredService<AppIdentityDbContext>();
    await IdentityContext.Database.MigrateAsync();
    await TalabatContextSeed.SeedData(context, loggerFactory); // data seeding

    var userManger = service.GetRequiredService<UserManager<AppUser>>();
    await AppIdentityDbContextSeed.SeedIdentityUser(userManger);
}
catch(Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an Error Occured During Apply Migration");
}
#endregion

#region Configure the HTTP request pipeline
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseStaticFiles(); // to use file in wwwroot 

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

#endregion

app.Run();
