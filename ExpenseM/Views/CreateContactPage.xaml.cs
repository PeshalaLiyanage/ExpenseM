using ExpenseM.UserControls;
using ExpenseM.Utilities;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using ExpenseM.Entities;
using ExpenseM.Models;
using System;


// TODO populate contact data when it loading
namespace ExpenseM.Views
{
  /// <summary>
  /// Interaction logic for CreateContactPage.xaml
  /// </summary>
  public partial class CreateContactPage : Page
  {
    UserModel contact = new UserModel();

    List<LabeledTextbox> labeledTextboxes = new List<LabeledTextbox>();
    List<LabelWithKey> labelNames = new List<LabelWithKey>
        {
            new LabelWithKey("fName",Properties.Resources.FIRST_NAME),
            new LabelWithKey("lName",Properties.Resources.LAST_NAME),
            new LabelWithKey("email",Properties.Resources.EMAIL),
            new LabelWithKey("address",Properties.Resources.ADDRESS),
            new LabelWithKey("phoneNo",Properties.Resources.PHONE_NUMBER)
        };

    public CreateContactPage()
    {
      InitializeComponent();
      this.WindowTitle = "Create Contacts";
      this.DataContext = this;

      contact.UserType = 0;
      this.CreateContactBtn.IsEnabled = false;
      foreach (LabelWithKey element in labelNames)
      {
        LabeledTextbox labeledTextbox = new LabeledTextbox();
        this.StackContent.Children.Add(labeledTextbox);

        labeledTextbox.Name = element.Key;
        labeledTextbox.LabelTitle.Content = element.LabelName;
        labeledTextbox.HorizontalAlignment = HorizontalAlignment.Center;
        labeledTextbox.UserInputbox.LostFocus += UserInput_LostFocus;

        Thickness thickness = new Thickness();
        thickness.Top = 10;
        labeledTextbox.Margin = thickness;

        labeledTextboxes.Add(labeledTextbox);

        switch (element.Key)
        {
          case "email":
            labeledTextbox.UserInputbox.TextChanged += ValidateEmail;
            break;
          default:
            break;

        }
      }
      this.PopulateDataToForm();
    }

    private void PopulateDataToForm()
    {
      Console.WriteLine("==============================================");
      UserModel tempUserData = new UserModel().getTempContactDataFromFile();

      if (tempUserData != null)
      {
        foreach (LabeledTextbox item in this.labeledTextboxes)
        {
          switch (item.Name)
          {
            case "fName":
              item.UserInputbox.Text = tempUserData.FirstName;
              break;
            case "lName":
              item.UserInputbox.Text = tempUserData.LastName;
              break;
            case "address":
              item.UserInputbox.Text = tempUserData.Address;
              break;
            case "email":
              item.UserInputbox.Text = tempUserData.Email;
              break;
            case "phoneNo":
              item.UserInputbox.Text = tempUserData.PhoneNumber;
              break;
            default:
              break;
          }
        }
      }
    }

    private void UserInput_LostFocus(object sender, RoutedEventArgs e)
    {
      UserModel tempUserModel = new UserModel();

      foreach (LabeledTextbox item in labeledTextboxes)
      {
        switch (item.Name)
        {
          case "fName":
            tempUserModel.FirstName = item.UserInputbox.Text;
            break;
          case "lName":
            tempUserModel.LastName = item.UserInputbox.Text;
            break;
          case "address":
            tempUserModel.Address = item.UserInputbox.Text;
            break;
          case "email":
            tempUserModel.Email = item.UserInputbox.Text;
            break;
          case "phoneNo":
            tempUserModel.PhoneNumber = item.UserInputbox.Text;
            break;
          default:
            break;
        }
      }

      tempUserModel.AddTempUserDataToFile(Properties.Resources.PATH_CONTACT_USER_DATA);
    }

    private void ClearBtn_Click(object sender, RoutedEventArgs e)
    {
      foreach (LabeledTextbox item in labeledTextboxes)
      {
        item.UserInputbox.Text = default;
      }
    }

    public void ValidateEmail(object sender, RoutedEventArgs e)
    {
      foreach (LabeledTextbox element in labeledTextboxes)
      {
        if (element.Name == "email")
        {
          if (!InputValidations.GetInstance.ValidateEmail(element.UserInputbox.Text))
          {
            this.CreateContactBtn.IsEnabled = false;

            element.UserInputbox.BorderBrush = Brushes.Red;
            element.UserInputbox.Background = Brushes.Red;
          }
          else
          {
            element.UserInputbox.BorderBrush = Brushes.CornflowerBlue;
            if (NullCheck())
            {
              this.CreateContactBtn.IsEnabled = true;
            }
          }

        }
      }
    }

    public bool NullCheck()
    {
      int nullCount = 0;
      foreach (LabeledTextbox element in labeledTextboxes)
      {
        if (element.UserInputbox.Text.Equals(null)
          || element.UserInputbox.Text.Equals(""))
        {
          nullCount++;
        }
      }

      if (nullCount != 0)
      {
        return false;
      }
      return true;
    }

    ExpenseMDataSet tempUserData = new ExpenseMDataSet();

    private void CreateContactBtn_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        foreach (LabeledTextbox element in labeledTextboxes)
        {
          // TODO -  add validation logics here
          if (element.Name == "email"
            && InputValidations.GetInstance.ValidateEmail(element.UserInputbox.Text))
          {
            contact.Email = element.UserInputbox.Text;
          }
          else if (element.Name == "fName")
          {
            contact.FirstName = element.UserInputbox.Text;
          }
          else if (element.Name == "lName")
          {
            contact.LastName = element.UserInputbox.Text;
          }
          else if (element.Name == "address")
          {
            contact.Address = element.UserInputbox.Text;
          }
          else if (element.Name == "phoneNo")
          {
            contact.PhoneNumber = element.UserInputbox.Text;
          }
        }
        contact.AddUser();
        MessageBox.Show(Properties.Resources.USER_ADDING_SUCCESS);
      }

      catch (Exception)
      {
        MessageBox.Show(Properties.Resources.SOMETHING_WRONG);
      }
    }

    private void RestoreBtn_Click(object sender, RoutedEventArgs e)
    {
      PopulateDataToForm();
    }
  }
}
