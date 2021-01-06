using ExpenseM.UserControls;
using ExpenseM.Utilities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ExpenseM.Views
{
  /// <summary>
  /// Interaction logic for CreateContactPage.xaml
  /// </summary>
  public partial class CreateContactPage : Page
  {
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

      foreach (LabelWithKey element in labelNames)
      {
        LabeledTextbox labeledTextbox = new LabeledTextbox();

        labeledTextbox.Name = element.Key;
        labeledTextbox.LabelTitle.Content = element.LabelName;
        labeledTextbox.HorizontalAlignment = HorizontalAlignment.Center;

        Thickness thickness = new Thickness();
      
        thickness.Top = 10;
        labeledTextbox.Margin = thickness ;
        

        labeledTextboxes.Add(labeledTextbox);
        this.StackContent.Children.Add(labeledTextbox);
      }

    }
 
  }
}
