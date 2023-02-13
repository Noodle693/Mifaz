using Microsoft.EntityFrameworkCore;
using Mifaz.Authorization;
using Mifaz.Database;
using Mifaz.Services;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddCors();
services.AddControllers();
services.AddScoped(_ => new PasswordHashingService());
services.AddScoped<IUserService, UserService>();
services.AddScoped<IRideService, RideService>();
services.AddScoped<IRideUserAssociationService, RideUserAssociationService>();
services.AddEntityFrameworkMySQL()
    .AddDbContext<MifazDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("Default") ??
                                                              throw new InvalidOperationException()));
services.AddSwaggerGen();
services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseMiddleware<BasicAuthMiddleware>();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run("http://*:5555");