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
      CalculateFInancialStatus();
    }

    public DateTime FromDate { get; set; } = DateUtilities.GetInstance.CurrentMonthStartDate();
    public DateTime ToDate { get; set; } = DateUtilities.GetInstance.CurrentMonthEndDate();

    public int TotalExpenses { get; set; }
    public int TotalIncome { get; set; }
    public string StatusMessage { get; set; } = "Ready";

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

    TransactionModel transactionModel = new TransactionModel();
    public List<TransactionModel> CommonTransactionList { get; set; }
    public List<TransactionModel> RecurringTransactionList { get; set; }
    private void CalculateFInancialStatus()
    {
      CommonTransactionList = FilterOneTimeTransactions(transactionModel.getTransactions(FromDate, ToDate));
      RecurringTransactionList = transactionModel.getTransactions(
        default(DateTime),
        DateUtilities.GetInstance.GetMonthStartDate(ToDate),
        true, true);

      int totalExpensesForMonth = 0;
      int totalIncomeForMonth = 0;

      foreach (TransactionModel item in CommonTransactionList)
      {
        if (item.TransactionType == EnumTransactionType.Expense)
        {
          totalExpensesForMonth += item.Amount;
        }
        else
        {
          totalIncomeForMonth += item.Amount;
        }
      }

      foreach (TransactionModel item in RecurringTransactionList)
      {
        if (item.TransactionType == EnumTransactionType.Expense)
        {
          totalExpensesForMonth += item.Amount;
        }
        else
        {
          totalIncomeForMonth += item.Amount;
        }
      }

      TotalExpenses = totalExpensesForMonth;
      TotalIncome = totalIncomeForMonth;

      OnPropertyChanged("TotalExpenses");
      OnPropertyChanged("TotalIncome");

    }

    private List<TransactionModel> FilterOneTimeTransactions(List<TransactionModel> transactionList)
    {
      List<TransactionModel> tempTransactions = new List<TransactionModel>();

      foreach (TransactionModel transaction in transactionList)
      {
        if (transaction.RecurrentStatus == 0)
        {
          tempTransactions.Add(transaction);
        }
      }

      return tempTransactions;
    }
  }
}
