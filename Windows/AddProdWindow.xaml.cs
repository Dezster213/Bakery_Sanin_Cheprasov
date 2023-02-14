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

namespace Bakery_Sanin_Cheprasov
{
   
    public partial class AddProdWindow : Window
    {
        private string pathPhoto = null;
        public AddProdWindow()
        {
            InitializeComponent();
            

            CMBTypeProduct.ItemsSource = Context.ProductType.ToList();
            CMBTypeProduct.SelectedIndex = 0;
            CMBTypeProduct.DisplayMemberPath = "TypeName";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Title = TbNameProduct.Text;
            product.Discription = TbDisc.Text;
            product.IDProdType = (CMBTypeProduct.SelectedItem as ProductType).IDprodType;
            if (pathPhoto != null)
            {
                product.ProductImage = File.ReadAllBytes(pathPhoto);
            }

            Context.Product.Add(product);

            Context.SaveChanges();
            MessageBox.Show("OK");
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
