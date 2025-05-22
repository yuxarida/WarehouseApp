using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WarehouseLibrary.Models;

namespace WarehouseApp
{
    public partial class EditProductWindow : Window
    {
        public List<string> Categories { get; } = new List<string>
        {
            "Продукти",
            "Побутова хімія",
            "Канцелярія",
            "Інструменти",
            "Одяг",
            "Електроніка",
            "Іграшки",
            "Меблі"
        };

        public Product Product { get; }

        public EditProductWindow(Product product)
        {
            InitializeComponent();
            Product = product;
            DataContext = Product;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(NameTextBox) ||
                Validation.GetHasError(CategoryComboBox) ||
                Validation.GetHasError(QuantityTextBox) ||
                Validation.GetHasError(PriceTextBox))
            {
                MessageBox.Show("Будь ласка, виправте помилки у формі.");
                return;
            }
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}