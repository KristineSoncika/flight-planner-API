using AutoMapper;
using FlightPlanner;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Data;
using FlightPlanner.Handlers;
using FlightPlanner.Models;
using FlightPlanner.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "admin-api", Version = "v1" });
    option.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter valid username and password",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Basic"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Basic"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthorisationHandler>("BasicAuthentication", null);

var connectionString = builder.Configuration.GetConnectionString("FlightPlannerConnection");
builder.Services.AddDbContext<FlightPlannerDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IFlightPlannerDbContext, FlightPlannerDbContext>();
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
builder.Services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IFlightValidator, FlightCarrierValidator>();
builder.Services.AddScoped<IFlightValidator, FlightTimeValidator>();
builder.Services.AddScoped<IFlightValidator, FlightAirportValidator>();
builder.Services.AddScoped<IAirportValidator, AirportCodeValidator>();
builder.Services.AddScoped<IAirportValidator, AirportCountryValidator>();
builder.Services.AddScoped<IAirportValidator, AirportCityValidator>();
builder.Services.AddScoped<IFlightSearchValidator, FlightSearchAirportValidator>();
builder.Services.AddScoped<IFlightSearchValidator, FlightSearchDepartureDateValidator>();
builder.Services.AddSingleton(AutoMapperConfig.CreateMapper());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();