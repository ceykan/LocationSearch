using System.Linq;
using System.Runtime.CompilerServices;
using LocationSearch;
using LocationSearch.BLL;
using LocationSearch.DAL;
using Moq;
using NUnit.Framework;

namespace LocationSearchTest
{
    public class Tests
    {
        private readonly LocationSearchAPI _locationSearchAPI;
        private const int MaxDistance = 10000;
        private const double Latitude = 69.6492047;
        private const double Longitude = 31.9357238;
        private const string Address = "Current";
        private const int MaxResults = 30;

        public Tests()
        {
            IRepository<Location> repository = new LocationRepository();
            IService<LocationSearchResult> service = new LocationService(repository);
            _locationSearchAPI = new LocationSearchAPI(service);
        }

        [Test]
        public void GetLocationsTestMaxResult()
        {
            Location loc = new Location(Latitude, Longitude, Address);
            var result = _locationSearchAPI.GetLocations(loc, null, MaxResults);
            Assert.AreEqual(result.Count, MaxResults);
        }

        [Test]
        public void GetLocationsTestMaxDistance()
        {
            Location loc = new Location(Latitude, Longitude, Address);
            var result = _locationSearchAPI.GetLocations(loc, MaxDistance, MaxResults);
            var listAny = result.Any(i => i.Distance > MaxDistance);
            Assert.False(listAny);
        }

        [Test]
        public void GetLocationsTestLimitless()
        {
            Location loc = new Location(Latitude, Longitude, Address);
            var result = _locationSearchAPI.GetLocations(loc, MaxDistance, null);
            Assert.GreaterOrEqual(result?.Count, 0);
        }

        /// <summary>
        /// A point from Istanbul Turkey, test should return 0 results accoring to the list.
        /// </summary>
        [Test]
        public void GetLocationsNonResult()
        {
            Location loc = new Location(41.046071941482005, 29.081998984554936, "Current");
            var result = _locationSearchAPI.GetLocations(loc, 1000, 1000);
            Assert.AreEqual(result?.Count, 0);
        }

        [Test]
        public void GetLocationsTestAscending()
        {
            Location loc = new Location(Latitude, Longitude, Address);
            var result = _locationSearchAPI.GetLocationsCloseToFar(loc, null, MaxResults);
            var expectedResult = result.OrderBy(i => i.Distance);
            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [Test]
        public void GetLocationsTestDescending()
        {
            Location loc = new Location(Latitude, Longitude, Address);
            var result = _locationSearchAPI.GetLocationsFromAFar(loc,null ,MaxResults);
            var expectedResult = result.OrderByDescending(i => i.Distance);
            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [Test]
        public void GetLocationsMaxDistanceNull()
        {
            Location loc = new Location(Latitude, Longitude, Address);
            var result = _locationSearchAPI.GetLocations(loc, null, null);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetLocationsMaxDistanceNullDescending()
        {
            Location loc = new Location(Latitude, Longitude, Address);
            var result = _locationSearchAPI.GetLocationsFromAFar(loc, null, MaxResults);
            var expectedResult = result.OrderByDescending(i => i.Distance);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }
    }
}