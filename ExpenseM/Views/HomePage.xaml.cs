using System;
using System.Collections.Generic;
using System.ComponentModel;
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
  /// Interaction logic for HomePage.xaml
  /// </summary>
  public partial class HomePage : Page, INotifyPropertyChanged
  {
    public HomePage()
    {
      InitializeComponent();
      this.WindowTitle = "ExpenseM";
      this.DataContext = this;
    }

    public DateTime FromDate { get; set; } = DateUtilities.GetInstance.CurrentMonthStartDate();
    public DateTime ToDate { get; set; } = DateUtilities.GetInstance.CurrentMonthEndDate();

    public int TotalExpenses { get; set; }
    public int TotalIncome { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string property)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(property));
      }
    }

    private void CreateContactBtn_Click(object sender, RoutedEventArgs e)
    {
      this.NavigationService.Navigate(new CreateContactPage());
    }

    private void NavAddExpenseBtn_Click(object sender, RoutedEventArgs e)
    {
      this.NavigationService.Navigate(new AddExpensesPage());
    }

    private void NavViewTransactions_Click(object sender, RoutedEventArgs e)
    {
      this.NavigationService.Navigate(new ViewTransactionsPage());
    }

    private void FromDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
    {

    }

    private void ToDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
    {

    }

    public List<TransactionModel> TransactionList { get; set; }
    public void CalculateFInancialStatus()
    {

    }
  }
}
