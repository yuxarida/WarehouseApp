using System.Collections.Generic;
using WarehouseLibrary.Models;

namespace WarehouseLibrary.Data
{
    public interface IDataManager
    {
        List<Product> LoadProducts();
        void SaveProducts(List<Product> products);
    }
}