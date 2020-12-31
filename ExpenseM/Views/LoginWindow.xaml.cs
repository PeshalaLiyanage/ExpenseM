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
using ExpenseM.Models;

namespace ExpenseM.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private String username;
        private String password;

        public LoginWindow()
        {
            InitializeComponent();
           
        }


        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow createUser = new CreateUserWindow();
            createUser.Show();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            username = this.UsernameInput.Text;
            password = this.PasswordInput.Password;

            UserModel user = new UserModel();

            bool validated = false;

            await Task.Run(() =>
            {
                try
                {
                    validated = user.ValidateUsernamePassword(username, password);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            if (validated == true)
            {
                HomeWindow home = new HomeWindow();
                home.Show();
                this.Close();
            }
        }
    }
}
