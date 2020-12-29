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

namespace ExpenseM.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private String _username;
        public LoginWindow()
        {
            InitializeComponent();
       

        }


        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow createUser = new CreateUserWindow();
            createUser.Show();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow home = new HomeWindow();
            home.Show();

            this.Close();

        }
    }
}
