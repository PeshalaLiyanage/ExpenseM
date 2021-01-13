using ExpenseM.Models;
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
using ExpenseM.Utilities;

namespace ExpenseM.UserControls
{
  /// <summary>
  /// User controller - transaction add rows
  /// </summary>
  public partial class ExpensesAddRow : UserControl
  {
    UserModel contact = new UserModel();
    List<UserModel> contacts = new List<UserModel>();
    public dynamic SelectedContact { get; set; }
    public dynamic SelectedTransactionType { get; set; }

    public string Amount { get; set; }
    public string Description { get; set; }

    private DateTime startDate = DateTime.Today;

    public DateTime SelectedStartDate
    {
      get { return startDate; }
      set { startDate = value; }
    }

    private dynamic endDate = null;

    public dynamic SelectedEndDate
    {
      get { return endDate; }
      set { endDate = value; }
    }

    public ExpensesAddRow()
    {
      InitializeComponent();

      this.ContactCombo.ItemsSource = contact.FetchUsers((int)UserTypes.Contact);
      this.TransactionTypeCombo.ItemsSource = new List<TransactionTypes>()
      {
        new TransactionTypes(0,"Income"),
        new TransactionTypes(1, "Expense")
      };
      this.DataContext = this;

    }
  }
}
