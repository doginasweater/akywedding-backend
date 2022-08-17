using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using akywedding_backend.Models.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(opt =>
{
  opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
  opt.TokenValidationParameters = new()
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["ISSUER"],
    ValidAudience = builder.Configuration["AUDIENCE"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SECRET_KEY"]))
  };
});

builder.Services.AddCors(opt =>
{
  opt.AddDefaultPolicy(policy =>
  {
    if (builder.Environment.IsProduction())
    {
      policy
        .WithOrigins("https://akyandrew2022.com", "https://www.akyandrew2022.com")
        .WithMethods("GET", "POST", "OPTIONS")
        .WithHeaders("content-type");
    }
    else
    {
      policy
        .AllowAnyOrigin()
        .WithMethods("GET", "POST", "OPTIONS")
        .WithHeaders("content-type");
    }
  });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<WeddingContext>(opt =>
  opt.UseNpgsql(builder.Configuration["CONNECTION_STRING"]));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseDeveloperExceptionPage();
  app.UseMigrationsEndPoint();
}
else
{
  app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var ctx = services.GetRequiredService<WeddingContext>();

  await ctx.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
