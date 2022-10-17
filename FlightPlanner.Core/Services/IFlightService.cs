using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner.Core.Services;

public interface IFlightService : IEntityService<Flight>
{
    Flight GetCompleteFlightById(int id);
    bool Exists(Flight flight);
    PageResult SearchFlight(FlightSearch search);
}