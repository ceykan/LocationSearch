using System.Collections.Generic;
using LocationSearch.DAL;

namespace LocationSearch.BLL
{
    public interface IService<T>
    {
        List<T> GetList(Location location, int ?maxDistance, int ?maxResults);
        List<T> GetListAscending(Location location, int ?maxDistance, int ?maxResults);
        List<T> GetListDescending(Location location, int ?maxDistance, int ?maxResults);
    }
}