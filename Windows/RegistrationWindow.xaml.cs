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
using Bakery_Sanin_Cheprasov.ClassHelper;
using Bakery_Sanin_Cheprasov.DB;
using Bakery_Sanin_Cheprasov.Windows;
using static Bakery_Sanin_Cheprasov.ClassHelper.EFClass;

namespace Bakery_Sanin_Cheprasov
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginBox.Text))
            {
                MessageBox.Show("Пустой логин");
                return;
            }
            if (string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                MessageBox.Show("Пустой Эмаил");
                return;
            }
            if (string.IsNullOrWhiteSpace(PassBox.Text))
            {
                MessageBox.Show("Пустой пароль");
                return;
            }
            Context.UserAccount.Add(new DB.UserAccount
            {
                Login = LoginBox.Text,
                Email = EmailBox.Text,
                Password = PassBox.Text,
                IdRole = 1,
            });
            Context.SaveChanges();
            MessageBox.Show("OK");
           
        }
    } 
}
