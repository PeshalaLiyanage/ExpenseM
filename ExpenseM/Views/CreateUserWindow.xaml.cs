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
      this.Title = "Create User";
      UserModel tempUserData = new UserModel().getTempUserDataFromFile();

      // Re populate cached data to form
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

      // Add user data to database using a different thread 
      await Task.Run(() =>
      {
        try
        {
          userModel.AddUser();
        }
        catch (Exception)
        {
          MessageBox.Show(Properties.Resources.SOMETHING_WRONG);
        }
      });

    }

    // Write user typing data to a file while user typing
    private void UserInput_LostFocus(object sender, RoutedEventArgs e)
    {
      userModel = new UserModel(
         this.FirstNameInputbox.Text,
         this.LastNameInputbox.Text,
         this.AddressInputbox.Text,
         this.PhoneNumberInputbox.Text,
         this.EmailInputbox.Text, 1
         );

      userModel.AddTempUserDataToFile(Properties.Resources.PATH_USER_TEMP_DATA);
    }
  }
}
