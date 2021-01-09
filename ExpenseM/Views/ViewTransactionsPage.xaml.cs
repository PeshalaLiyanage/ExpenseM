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

namespace ExpenseM.Views
{
  /// <summary>
  /// Interaction logic for ViewTransactionsPage.xaml
  /// </summary>
  public partial class ViewTransactionsPage : Page
  {
    public ViewTransactionsPage()
    {
      InitializeComponent();
      this.WindowTitle = "Transactions";
      this.DataContext = this;
      TransactionModel transactionModel = new TransactionModel();
      TransactionList= transactionModel.getTransactions();
    }

    public List<TransactionModel> TransactionList { get; set; }





  }
}
