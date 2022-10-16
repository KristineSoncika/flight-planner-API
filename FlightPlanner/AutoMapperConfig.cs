using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner;

public class AutoMapperConfig
{
    public static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(d => d.Id, options => options
                        .Ignore())
                    .ForMember(d => d.AirportCode, options => options
                        .MapFrom(s => s.Airport));
                cfg.CreateMap<Airport, AirportRequest>()
                    .ForMember(d => d.Airport, options => options
                        .MapFrom(s => s.AirportCode));
                cfg.CreateMap<FlightRequest, Flight>();
                cfg.CreateMap<Flight, FlightRequest>();
            });
        
        config.AssertConfigurationIsValid();
        
        return config.CreateMapper();
    }
}