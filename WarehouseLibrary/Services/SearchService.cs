using System.Collections.Generic;
using System.Linq;
using WarehouseLibrary.Models;

namespace WarehouseLibrary.Services
{
    public class SearchService : ISearchService
    {
        public List<Product> Search(IEnumerable<Product> products, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return products.ToList();

            return products
                .Where(p => p.Name.Contains(query, System.StringComparison.OrdinalIgnoreCase)
                         || p.Category.Contains(query, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}