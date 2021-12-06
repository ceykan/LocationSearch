using System.Collections.Generic;
using System.Linq;
using LocationSearch.DAL;

namespace LocationSearch.BLL
{
    public class LocationService : IService<LocationSearchResult>
    {
        private readonly List<Location> _locations;

        public LocationService(IRepository<Location> locationRepository)
        {
            _locations = locationRepository.GetAll();
        }

        public List<LocationSearchResult> GetList(Location currentLocation, int? maxDistance, int? maxResults)
        {
            var distancesCalculated = from a in _locations
                select new LocationSearchResult
                {
                    Distance = a.CalculateDistance(currentLocation),
                    Location = new Location(a.Latitude, a.Longitude, a.Address)
                };
            var result = distancesCalculated.ToList();
            if (maxDistance.HasValue)
            {
                result = result.Where(i => i.Distance <= maxDistance).ToList();
            }
            if (maxResults.HasValue)
            {
                result = result.Take(maxResults.Value).ToList();
            }
            return result;
        }

        public List<LocationSearchResult> GetListAscending(Location currentLocation, int ?maxDistance, int? maxResults)
        {
            var result = GetList(currentLocation, maxDistance, maxResults);
            return result.OrderBy(i => i.Distance).ToList();
        }

        public List<LocationSearchResult> GetListDescending(Location currentLocation, int ?maxDistance, int? maxResults)
        {
            var result = GetList(currentLocation, maxDistance, maxResults);
            return result.OrderByDescending(i => i.Distance).ToList();
        }
    }
}