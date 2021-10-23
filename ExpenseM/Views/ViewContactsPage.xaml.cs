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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExpenseM.Models;
using ExpenseM.Utilities;

namespace ExpenseM.Views
{
  /// <summary>
  /// Interaction logic for ViewContactsPage.xaml
  /// </summary>
  public partial class ViewContactsPage : Page
  {

    UserModel userModel = new UserModel();
    public ViewContactsPage()
    {
      InitializeComponent();
      this.DataContext = this;
      this.WindowTitle = "Contacts";
      ContactsList = userModel.FetchUsers((int)UserTypes.Contact); // load contacts
    }
    public List<UserModel> ContactsList { get; set; }
  }
}
