using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WarehouseLibrary.Models;

namespace WarehouseLibrary.Data
{
    public class DataManager : IDataManager
    {
        private readonly string _filePath;

        public DataManager(string filePath)
        {
            _filePath = filePath;
        }

        public List<Product> LoadProducts()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Product>
                {
                    new Product { Name = "Test1", Category = "Електроніка", Quantity = 5, Price = 1500 },
                    new Product { Name = "Test2", Category = "Меблі", Quantity = 20, Price = 80 },
                    new Product { Name = "Test3", Category = "Продукти", Quantity = 100, Price = 3 }
                };
            }

            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при читанні файлу: {ex.Message}");
                return new List<Product>();
            }
        }

        public void SaveProducts(List<Product> products)
        {
            try
            {
                var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при збереженні файлу: {ex.Message}");
            }
        }
    }
}