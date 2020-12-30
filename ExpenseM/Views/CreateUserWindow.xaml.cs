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
using ExpenseM.Entities;
using ExpenseM.Utilities;
using System.IO;

namespace ExpenseM.Views
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        UserModel userModel;
        ExpenseMDataSet tempUserData = new ExpenseMDataSet();

        public CreateUserWindow()
        {
            InitializeComponent();

            UserModel tempUserData = new UserModel().getTempUserDataFromFile();

            if (tempUserData != null)
            {
                this.FirstNameInputbox.Text = tempUserData.FirstName;
                this.LastNameInputbox.Text = tempUserData.LastName;
                this.AddressInputbox.Text = tempUserData.Address;
                this.PhoneNumberInputbox.Text = tempUserData.PhoneNumber;
                this.EmailInputbox.Text = tempUserData.Email;
            }
        }

        private async void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            userModel = new UserModel(
                this.FirstNameInputbox.Text,
                this.LastNameInputbox.Text,
                this.AddressInputbox.Text,
                this.PhoneNumberInputbox.Text,
                this.EmailInputbox.Text, 1
                );

            userModel.Password = this.PasswordInputbox.Password;

            await Task.Run(() =>
            {
                try
                {
                    userModel.AddUser();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

        }

        private void UserInput_LostFocus(object sender, RoutedEventArgs e)
        {
            userModel = new UserModel(
               this.FirstNameInputbox.Text,
               this.LastNameInputbox.Text,
               this.AddressInputbox.Text,
               this.PhoneNumberInputbox.Text,
               this.EmailInputbox.Text, 1
               );

            userModel.AddTempUserDataToFile();
        }
    }
}
