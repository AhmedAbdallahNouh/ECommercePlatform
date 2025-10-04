using ECommerce.API.Endpoints;
using ECommerce.Application.Behaviors;
using ECommerce.Infrastructure.Persistence.DbContexts;
using ECommerce.Infrastructure.SeedData;
using FluentValidation;
using MediatR;
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

builder.Services.AddValidatorsFromAssembly(ECommerce.Application.AssemblyReference.Assembly, includeInternalTypes: true);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<WriteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WriteConnection")));

builder.Services.AddDbContext<ReadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReadConnection")));


//builder.Services.AddDbContext<ECommerceDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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


app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features
            .Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationEx)
        {
            var errors = validationEx.Errors.Select(e => new
            {
                Field = e.PropertyName,
                Error = e.ErrorMessage
            });

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Validation failed",
                Errors = errors
            });
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Message = "An error occurred" });
        }
    });
});


app.Run();

