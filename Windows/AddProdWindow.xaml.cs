using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bakery_Sanin_Cheprasov.DB;
using Bakery_Sanin_Cheprasov.Windows;
using static Bakery_Sanin_Cheprasov.ClassHelper.EFClass;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.ComponentModel;

namespace Bakery_Sanin_Cheprasov
{
   
    public partial class AddProdWindow : Window
    {
        private string pathPhoto = null;

        private bool isEdit = false;

        private Product editProduct;



        public AddProdWindow()
        {
            InitializeComponent();

            CMBTypeProduct.ItemsSource = Context.ProductType.ToList();
            CMBTypeProduct.SelectedIndex = 0;
            CMBTypeProduct.DisplayMemberPath = "TypeName";
            tb1.Text = "Добавление товара";
            BtnAddEdit.Content = "Добавить";
        }

        public AddProdWindow(Product product)
        {
            InitializeComponent();

            editProduct = product;

            CMBTypeProduct.ItemsSource = Context.ProductType.ToList();
            CMBTypeProduct.SelectedIndex = 0;
            CMBTypeProduct.DisplayMemberPath = "TypeName";

            TbNameProduct.Text = product.Title.ToString();
            TbDisc.Text = product.Description.ToString();
            TbCost.Text = product.Cost.ToString();
            check.IsChecked = product.Active;
            CMBTypeProduct.SelectedItem = Context.ProductType.Where(i => i.IDprodType == product.ProductTypeid).FirstOrDefault();
            if (product.Image != null)
            {
                using (MemoryStream stream = new MemoryStream(product.Image))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    ImgProduct.Source = bitmapImage;

                }
            }
            isEdit = true;
        }

        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            // валидация


            if (isEdit)
            {
                //изменение товара


                editProduct.Title = TbNameProduct.Text;
                editProduct.Description = TbDisc.Text;
                editProduct.Cost = Convert.ToDecimal(TbCost.Text);
                editProduct.ProductTypeid = (CMBTypeProduct.SelectedItem as ProductType).IDprodType;
                editProduct.Active = (bool)check.IsChecked;
                if (pathPhoto != null)
                {
                    editProduct.Image = File.ReadAllBytes(pathPhoto);
                }
                Context.SaveChanges();
                MessageBox.Show("OK");
            }
            else
            {
                Product product = new Product();
                product.Title = TbNameProduct.Text;
                product.Description = TbDisc.Text;
                product.Cost = Convert.ToDecimal(TbCost.Text);
                product.ProductTypeid = (CMBTypeProduct.SelectedItem as ProductType).IDprodType;
                product.Active = (bool)check.IsChecked;
                if (pathPhoto != null)
                {
                    product.Image = File.ReadAllBytes(pathPhoto);
                }

                Context.Product.Add(product);

                Context.SaveChanges();
                MessageBox.Show("OK");
            }
            ListOfProductsWindow productlistWindow = new ListOfProductsWindow();

            this.Close();

        }

        private void BtnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgProduct.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                pathPhoto = openFileDialog.FileName;
            }
           
        }
    }
}
