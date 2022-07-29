using Microsoft.EntityFrameworkCore;
using akywedding_backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<WeddingContext>(opt =>
  opt.UseNpgsql(builder.Configuration["CONNECTION_STRING"]));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
