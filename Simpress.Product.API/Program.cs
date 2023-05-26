using Microsoft.EntityFrameworkCore;
using Simpress.Product.API.Configuration;
using Simpress.Product.API.EndPoints;
using Simpress.Product.Application;
using Simpress.Product.Infra.Database;
using Simpress.Product.Infra.Database.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddDbContext<SimpressContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SimpressConnection")));



DatabaseModuleDependecy.AddDatabaseModule(builder.Services);
ApplicationModuleDependency.AddApplicationModule(builder.Services);

var app = builder.Build();

using (var scope =app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SimpressContext>(); 
    
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCategorysEndPoints();

app.MapProductsEndPoints();

app.Run();

