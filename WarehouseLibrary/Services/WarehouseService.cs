using System.Collections.Generic;
using System.Linq;
using WarehouseLibrary.Models;
using WarehouseLibrary.Data;

namespace WarehouseLibrary.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IDataManager _dataManager;
        private readonly ISearchService _searchService;
        private List<Product> _products;

        public WarehouseService(IDataManager dataManager, ISearchService searchService)
        {
            _dataManager = dataManager;
            _searchService = searchService;
            _products = _dataManager.LoadProducts();
        }

        public IEnumerable<Product> GetAll() => _products;

        public void Add(Product p)
        {
            _products.Add(p);
            Save();
        }

        public void Edit(Product old, Product updated)
        {
            var idx = _products.IndexOf(old);
            if (idx != -1)
            {
                _products[idx] = updated;
                Save();
            }
        }

        public void Delete(Product p)
        {
            _products.Remove(p);
            Save();
        }

        public IEnumerable<Product> Search(string query) =>
            _searchService.Search(_products, query);

        public void Save() => _dataManager.SaveProducts(_products);
    }
}