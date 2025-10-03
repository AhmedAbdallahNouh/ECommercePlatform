using ECommerce.API.Endpoints;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Infrastructure.Persistence.DbContext;
using ECommerce.Infrastructure.Persistence.UniteOfWork;
using ECommerce.Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Scan(scan => scan
    .FromAssemblies(
        ECommerce.Infrastructure.AssemblyReference.Assembly)
    .AddClasses(false)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(ECommerce.Application.AssemblyReference.Assembly);
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Seed Data

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
    DbInitializer.Seed(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

// Register endpoints
app.MapRoductsEndPointsAsync();
// app.MapCategoriesEndpoints();
// app.MapOrdersEndpoints();
// app.MapPaymentsEndpoints();
// app.MapCartsEndpoints();
// app.MapNotificationsEndpoints();
// app.MapReviewsEndpoints();

app.UseHttpsRedirection();

app.MapGet("/", () => "E-Commerce API Running 🚀");


app.Run();

