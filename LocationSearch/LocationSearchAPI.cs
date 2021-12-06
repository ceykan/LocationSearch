using System.Collections.Generic;
using LocationSearch.BLL;
using LocationSearch.DAL;

namespace LocationSearch
{
    public class LocationSearchAPI
    {
        private readonly IService<LocationSearchResult> _locationService;

        public LocationSearchAPI(IService<LocationSearchResult> locationService)
        {
            _locationService = locationService;
        }

        public List<LocationSearchResult> GetLocations(Location location, int? maxDistance, int? maxResults)
        {
            return _locationService.GetList(location, maxDistance, maxResults);
        }

        public List<LocationSearchResult> GetLocationsFromAFar(Location location, int ?maxDistance, int ?maxResults)
        {
            return _locationService.GetListDescending(location, maxDistance, maxResults);
        }

        public List<LocationSearchResult> GetLocationsCloseToFar(Location location, int ?maxDistance, int ?maxResults)
        {
            return _locationService.GetListAscending(location, maxDistance, maxResults);
        }
    }
}