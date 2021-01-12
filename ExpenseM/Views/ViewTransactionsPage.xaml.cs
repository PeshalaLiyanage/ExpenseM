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
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

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
   
    
  }
}
