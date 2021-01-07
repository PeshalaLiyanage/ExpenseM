using ExpenseM.UserControls;
using ExpenseM.Utilities;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using ExpenseM.Entities;
using ExpenseM.Controllers;
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

    private String _firstName;
    private String _lastName;
    private String _address;
    private String _phoneNumber;
    private String _email;
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

    //private bool buttonEnabled = false;

    //public bool ButtonEnabled
    //{
    //  get { return buttonEnabled; }
    //  set { buttonEnabled = value; }
    //}

    public CreateContactPage()
    {
      InitializeComponent();
      contact.UserType = 0;
      this.CreateContactBtn.IsEnabled = false;
      foreach (LabelWithKey element in labelNames)
      {
        LabeledTextbox labeledTextbox = new LabeledTextbox();
        this.StackContent.Children.Add(labeledTextbox);

        labeledTextbox.Name = element.Key;
        labeledTextbox.LabelTitle.Content = element.LabelName;
        labeledTextbox.HorizontalAlignment = HorizontalAlignment.Center;

        Thickness thickness = new Thickness();
        thickness.Top = 10;
        labeledTextbox.Margin = thickness;

        labeledTextboxes.Add(labeledTextbox);

        switch (element.Key)
        {
          case "email":
            _email = labeledTextbox.UserInputbox.Text;
            labeledTextbox.UserInputbox.TextChanged += ValidateEmail;
            break;
          default:
            break;

        }
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
      // todo - add email duplicate check validation
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
          else
          {
            // throw new Exception("Invalid user inputs");
          }

        }
        contact.AddUser();
      }
      finally { }
      //catch (Exception ex)
      //{
      //  MessageBox.Show(ex.Message);
      //}



    }
  }
}
