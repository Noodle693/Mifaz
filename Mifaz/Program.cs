using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Mifaz.Authorization;
using Mifaz.Database;
using Mifaz.Services;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5555";
var url = $"http://0.0.0.0:{port}";

var services = builder.Services;
services.AddCors();
services.AddControllers();
services.AddScoped(_ => new PasswordHashingService());
services.AddEntityFrameworkMySQL()
    .AddDbContext<MifazDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("Default") ??
                                                              throw new InvalidOperationException()));
services.AddScoped<IUserService, UserService>();
services.AddScoped<IRideService, RideService>();
services.AddSwaggerGen();
services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseMiddleware<BasicAuthMiddleware>();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run(url);

//mifazdb;rootpw