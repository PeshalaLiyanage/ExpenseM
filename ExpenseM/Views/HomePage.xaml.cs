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
      this.Title = "ExpenseM";
      this.DataContext = this;
      CalculateFInancialStatus();
    }

    TransactionModel transactionModel = new TransactionModel();
    public DateTime FromDate { get; set; } = DateUtilities.GetInstance.CurrentMonthStartDate();
    public DateTime ToDate { get; set; } = DateUtilities.GetInstance.CurrentMonthEndDate();
    public int TotalExpenses { get; set; }
    public int TotalIncome { get; set; }
    public string StatusMessage { get; set; } = Properties.Resources.READY_MESSAGE; // use resource file
    public List<TransactionModel> CommonTransactionList { get; set; }
    public List<TransactionModel> RecurringTransactionList { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    // notify property changes
    protected void OnPropertyChanged(string property)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(property));
      }
    }

    // Navigation functions
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

    private void ContactsViewNav_Click(object sender, RoutedEventArgs e)
    {
      this.NavigationService.Navigate(new ViewContactsPage());
    }

    private void FromDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
    {
      OnPropertyChanged("FromDate");
      if (FromDate > DateUtilities.GetInstance.CurrentMonthEndDate()
        || ToDate > DateUtilities.GetInstance.CurrentMonthEndDate())
      {
        PredictFinancialStatus();
      }
      else
      {
        CalculateFInancialStatus();
      }
    }

    private void ToDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
    {
      OnPropertyChanged("ToDate");

      if (FromDate > DateUtilities.GetInstance.CurrentMonthEndDate()
        || ToDate > DateUtilities.GetInstance.CurrentMonthEndDate())
      {
        PredictFinancialStatus();
      }
      else
      {
        CalculateFInancialStatus();
      }

    }

    private void CalculateFInancialStatus()
    {
      StatusMessage = Properties.Resources.PROCESSING;
      OnPropertyChanged("StatusMessage");
      int totalExpensesForMonth = 0;
      int totalIncomeForMonth = 0;

      // Get only one time transactions
      CommonTransactionList = FilterOneTimeTransactions(transactionModel.getTransactions(FromDate, ToDate));

      // Get only recurrent transactions
      RecurringTransactionList = transactionModel.getTransactions(
        default(DateTime),
        DateUtilities.GetInstance.GetMonthStartDate(ToDate),
        true, true);

      // calculate total one time trancation amount
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

      // calculate total recurrent trancation amount
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
      StatusMessage = Properties.Resources.READY_MESSAGE;
      OnPropertyChanged("StatusMessage"); // notify
      OnPropertyChanged("TotalExpenses");
      OnPropertyChanged("TotalIncome");

    }

    // The prediction algorithm
    private void PredictFinancialStatus()
    {
      StatusMessage = Properties.Resources.PREDICTING;
      OnPropertyChanged("StatusMessage");

      CommonTransactionList = FilterOneTimeTransactions(transactionModel.getTransactions(default, default));

      DateTime allRecordsBeginDate = DateTime.Today;
      DateTime allRecordsEndDate = default;

      int totalExpenses = 0;
      int totalIncome = 0;
      int averageIncomePerMonth = 0;
      int averageExpensePerMonth = 0;

      foreach (TransactionModel item in CommonTransactionList)
      {
        if (item.StartDate < allRecordsBeginDate)
        {
          allRecordsBeginDate = item.StartDate;
        }

        if (item.StartDate > allRecordsEndDate)
        {
          allRecordsEndDate = item.StartDate;
        }
        if (item.TransactionType == EnumTransactionType.Expense)
        {
          totalExpenses += item.Amount;
        }
        if (item.TransactionType == EnumTransactionType.Income)
        {
          totalIncome += item.Amount;
        }
      }

      int monthDifference = DateUtilities.GetInstance.GetMonthDifference(allRecordsBeginDate, allRecordsEndDate);

      averageIncomePerMonth = totalIncome / monthDifference;
      averageExpensePerMonth = totalExpenses / monthDifference;

      int recurrentIncomeAmount = 0;
      int recurrentExpenseAmount = 0;
      int userSelectedDateDifference = DateUtilities.GetInstance.GetMonthDifference(FromDate, ToDate);

      foreach (TransactionModel item in RecurringTransactionList)
      {

        if (item.TransactionType == EnumTransactionType.Income)
        {
          if (item.StartDate < FromDate)
          {
            recurrentIncomeAmount += item.Amount * userSelectedDateDifference;
          }
          else
          {
            recurrentIncomeAmount += item.Amount * DateUtilities.GetInstance.GetMonthDifference(
              DateUtilities.GetInstance.GetMonthStartDate(item.StartDate), 
              DateUtilities.GetInstance.GetMonthEndDate(item.EndDate));
          }
        }
        else
        {
          if (item.StartDate < FromDate)
          {
            recurrentExpenseAmount += item.Amount * userSelectedDateDifference;
          }
          else
          {
            recurrentExpenseAmount += item.Amount * DateUtilities.GetInstance.GetMonthDifference(
              DateUtilities.GetInstance.GetMonthStartDate(item.StartDate), 
              DateUtilities.GetInstance.GetMonthEndDate(item.EndDate));
          }
        }

      }

      TotalIncome = averageIncomePerMonth * userSelectedDateDifference + recurrentIncomeAmount;
      TotalExpenses = averageExpensePerMonth * userSelectedDateDifference + recurrentExpenseAmount;

      OnPropertyChanged("TotalIncome");
      OnPropertyChanged("TotalExpenses");
      StatusMessage = Properties.Resources.PREDICTION_SUCCESS;
      OnPropertyChanged("StatusMessage");
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
