using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LocationSearch.Common;

namespace LocationSearch.DAL
{
    public class LocationRepository : IRepository<Location>
    {
        public List<Location> GetAll()
        {
            var lines = LocationEntity.ReadFromFile();
            var stuff =
                (from l in lines
                    select new Location(
                        Double.Parse(l[1].Replace('"', ' '), CultureInfo.InvariantCulture),
                        Double.Parse(l[2].Replace('"', ' '), CultureInfo.InvariantCulture),
                        l[0].Replace('"', ' ').Trim()))
                .ToList();
            return stuff;
        }
    }
}