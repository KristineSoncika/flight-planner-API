using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IClearDb: IDbService
{
    void ClearDatabase();
}