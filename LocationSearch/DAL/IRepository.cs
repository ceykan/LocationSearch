using System.Collections.Generic;
using System.Dynamic;

namespace LocationSearch
{
    public interface IRepository<T>
    {
        List<T> GetAll();
    }
}