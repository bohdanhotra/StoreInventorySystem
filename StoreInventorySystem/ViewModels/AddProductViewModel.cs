using System;
using System.Windows.Input;
using Microsoft.Win32;
using StoreInventorySystem.Commands;
using StoreInventorySystem.Models;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.ViewModels
{
    public class AddProductViewModel
    {
        public Product NewProduct { get; set; } = new Product();
        public ICommand SelectImageCommand { get; }
        public ICommand SaveCommand { get; }

        public AddProductViewModel()
        {
            SelectImageCommand = new RelayCommand(_ => {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (op.ShowDialog() == true) NewProduct.ImagePath = op.FileName;
            });

            SaveCommand = new RelayCommand(_ => {
                var products = ProductService.LoadProducts();

                // Копіюємо картинку локально
                if (!string.IsNullOrEmpty(NewProduct.ImagePath))
                    NewProduct.ImagePath = ProductService.SaveImage(NewProduct.ImagePath);

                NewProduct.Id = Guid.NewGuid().ToString().Substring(0, 8);
                products.Add(NewProduct);
                ProductService.SaveProducts(products);

                System.Windows.MessageBox.Show("Товар збережено!");
            });
        }
    }
}