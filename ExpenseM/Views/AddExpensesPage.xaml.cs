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
using ExpenseM.UserControls;

namespace ExpenseM.Views
{
  /// <summary>
  /// Interaction logic for AddExpensesPage.xaml
  /// </summary>
  public partial class AddExpensesPage : Page
  {

    private int totalRowsCount = 0;
    private const int MaxRowsCount = 12;

    List<ExpensesAddRow> labeledTextboxes = new List<ExpensesAddRow>();
    public AddExpensesPage()
    {
      InitializeComponent();
     //create user control here and after creating it, move it to the user control file(expense add)
    }

    private void AddRowsBtn_Click(object sender, RoutedEventArgs e)
    {
      int rowsCount = int.Parse(this.RowCountBox.Text);
     

      if (rowsCount<= MaxRowsCount && (totalRowsCount += rowsCount) <= MaxRowsCount)
      {
        //totalRowsCount += rowsCount;
        for (int i = 0; i < rowsCount; i++)
        {
          ExpensesAddRow expensesAddRow = new ExpensesAddRow();
          Thickness thickness = new Thickness();
          thickness.Top = 10;
          expensesAddRow.Margin = thickness;
          this.MainPannel.Children.Add(expensesAddRow);
        }

      }
      else
      {
        MessageBox.Show("You can add only maximum 12 records at once.");
      }
    }
  }
}
