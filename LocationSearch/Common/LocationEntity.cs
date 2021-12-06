using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LocationSearch.Common
{
    public static class LocationEntity
    {
        public static IEnumerable<string[]> ReadFromFile()
        {
            var path = Path.Combine(AppContext.BaseDirectory
                , "locations.csv");
            var lines = File.ReadAllLines(path)
                .Skip(1)
                .Where(row => row.Length > 0).Select(i => Regex.Split(i, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)")).ToList();
            return lines;
        }
    }
}