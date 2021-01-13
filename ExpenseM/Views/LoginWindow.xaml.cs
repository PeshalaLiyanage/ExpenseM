﻿using System;
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
using ExpenseM.Utilities;

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
      // remove this values
      this.UsernameInput.Text = "pesh.com";
      this.PasswordInput.Password = "123";


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
        MainWindow home = new MainWindow();
        home.Show();
        this.Close();
      }
      else
      {
        MessageBox.Show("User not found for " + username, this.Title);
      }
    }
    public void ValidateEmail(object sender, RoutedEventArgs e)
    {
      if (!InputValidations.GetInstance.ValidateEmail(this.UsernameInput.Text))
      {
        this.LoginBtn.IsEnabled = false;

        this.UsernameInput.BorderBrush = Brushes.Red;
        this.UsernameInput.Background = Brushes.Red;
      }
      else
      {
        this.UsernameInput.BorderBrush = Brushes.CornflowerBlue;
        this.UsernameInput.Background = Brushes.White;
        this.LoginBtn.IsEnabled = true;
      }

    }

    private void PasswordChanged(object sender, RoutedEventArgs e)
    {
      if (this.PasswordInput.Password.Equals(""))
      {
        this.LoginBtn.IsEnabled = false;

        this.PasswordInput.BorderBrush = Brushes.Red;
        this.PasswordInput.Background = Brushes.Red;
      }
      else
      {
        this.PasswordInput.BorderBrush = Brushes.CornflowerBlue;
        this.PasswordInput.Background = Brushes.White;
        this.LoginBtn.IsEnabled = true;
      }
    }
  }

}
