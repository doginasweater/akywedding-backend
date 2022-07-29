using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using akywedding_backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddAuthentication()
//   .AddJwtBearer(opt => {
//     if (builder.Environment.IsDevelopment()) {
//       opt.Audience = "http://localhost:5173";
//       opt.Authority = "http://localhost:7149";
//     }
//   });

builder.Services.AddCors(opt => {
  opt.AddDefaultPolicy(policy => {
    if (builder.Environment.IsProduction()) {
      policy.WithOrigins("https://akyandrew2022.com", "https://www.akyandrew2022.com");
    } else {
      policy.AllowAnyOrigin();
    }
  });
});

builder.Services.AddControllers();

if (builder.Environment.IsProduction()) {
  builder.Services.AddDbContext<WeddingContext>(opt =>
    opt.UseNpgsql(builder.Configuration["CONNECTION_STRING"]));
} else {
  builder.Services.AddDbContext<WeddingContext>(opt =>
    opt.UseSqlite("Data Source=wedding.db"));
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseDeveloperExceptionPage();
  app.UseMigrationsEndPoint();
} else {
  app.UseHsts();
}

using (var scope = app.Services.CreateScope()) {
  var services = scope.ServiceProvider;
  var ctx = services.GetRequiredService<WeddingContext>();

  await ctx.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
