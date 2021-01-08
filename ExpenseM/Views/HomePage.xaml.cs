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

namespace ExpenseM.Views
{
  /// <summary>
  /// Interaction logic for HomePage.xaml
  /// </summary>
  public partial class HomePage : Page
  {
    public HomePage()
    {
      InitializeComponent();
      this.WindowTitle = "ExpenseM";
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
  }
}
