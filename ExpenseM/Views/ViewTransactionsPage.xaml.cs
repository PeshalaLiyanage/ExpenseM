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
using System.ComponentModel;
using ExpenseM.Models;
using ExpenseM.Utilities;

namespace ExpenseM.Views
{
  /// <summary>
  /// Interaction logic for ViewTransactionsPage.xaml
  /// </summary>
  public partial class ViewTransactionsPage : Page, INotifyPropertyChanged
  {

    TransactionModel transactionModel = new TransactionModel();

    public event PropertyChangedEventHandler PropertyChanged;

    public ViewTransactionsPage()
    {
      InitializeComponent();
      this.WindowTitle = "Transactions";
      this.DataContext = this;
      
      TransactionList= transactionModel.getTransactions();
      tempTransactionList = TransactionList;
    }
    protected void OnPropertyChanged(string property)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(property));
      }
    }

    public List<TransactionModel> TransactionList { get; set; }
    public List<TransactionModel> tempTransactionList;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public bool ExpenseChecked { get; set; } = true;
    public bool IncomeChecked { get; set; } = true;
    public bool RecurringChecked { get; set; } = false;

    private void SetStartDate(object sender, RoutedEventArgs e)
    {
      FromDate = (DateTime)this.StartDatePicker.SelectedDate;
      TransactionList = transactionModel.getTransactions(FromDate,ToDate);
      OnPropertyChanged("TransactionList");
    }

    private void SetEndDate(object sender, RoutedEventArgs e)
    {
      ToDate = (DateTime)this.EndDatePicker.SelectedDate;
      TransactionList = transactionModel.getTransactions( FromDate,ToDate);
      OnPropertyChanged("TransactionList");
    }

  

    private void SelectType(object sender, RoutedEventArgs e)
    {
      if (ExpenseChecked == true && IncomeChecked == true || ExpenseChecked == false && IncomeChecked == false)
      {
        TransactionList = tempTransactionList;
      }
      else if(ExpenseChecked == true)
      {
        TransactionList = FilterTransactions(tempTransactionList, EnumTransactionType.Expense);
      }
      else if (IncomeChecked == true)
      {
        TransactionList = FilterTransactions(tempTransactionList, EnumTransactionType.Income);
      }
      OnPropertyChanged("TransactionList");

    }

    private List<TransactionModel> FilterTransactions(List<TransactionModel> list, EnumTransactionType type)
    {
      List<TransactionModel> tempList = new List<TransactionModel>();

      foreach (TransactionModel item in list)
      {
        if (item.TransactionType == type)
        {
          tempList.Add(item);
        }
      }

      return tempList;
    }

    private async void RecurringCheckbox_Click(object sender, RoutedEventArgs e)
    {
      await Task.Run(()=> {
        if (RecurringChecked == true)
        {
          TransactionList = transactionModel.getTransactions(FromDate, ToDate, true);
          tempTransactionList = TransactionList;
          OnPropertyChanged("TransactionList");
        }
        else
        {
          TransactionList = transactionModel.getTransactions(FromDate, ToDate);
          tempTransactionList = TransactionList;
          OnPropertyChanged("TransactionList");
        }
      });

     
    }
  }
}
