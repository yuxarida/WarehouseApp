using System.ComponentModel;

namespace WarehouseLibrary.Models
{
    public class Product : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _name = string.Empty;
        private string _category = string.Empty;
        private int _quantity;
        private double _price;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(nameof(Category)); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value < 0 ? 0 : value; OnPropertyChanged(nameof(Quantity)); }
        }

        public double Price
        {
            get => _price;
            set { _price = value < 0 ? 0 : value; OnPropertyChanged(nameof(Price)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // IDataErrorInfo для валідації
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    nameof(Name) => string.IsNullOrWhiteSpace(Name) ? "Введіть назву" : null,
                    nameof(Category) => string.IsNullOrWhiteSpace(Category) ? "Оберіть категорію" : null,
                    nameof(Quantity) => Quantity < 0 ? "Кількість >= 0" : null,
                    nameof(Price) => Price < 0 ? "Ціна >= 0" : null,
                    _ => null
                };
            }
        }
    }
}