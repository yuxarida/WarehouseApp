using System.Collections.Generic;
using WarehouseLibrary.Models;

namespace WarehouseLibrary.Services
{
    public interface IWarehouseService
    {
        IEnumerable<Product> GetAll();
        void Add(Product p);
        void Edit(Product old, Product updated);
        void Delete(Product p);
        IEnumerable<Product> Search(string query);
        void Save();
    }
}