using System.Collections.Generic;
using WarehouseLibrary.Models;

namespace WarehouseLibrary.Services
{
    public interface ISearchService
    {
        List<Product> Search(IEnumerable<Product> products, string query);
    }
}