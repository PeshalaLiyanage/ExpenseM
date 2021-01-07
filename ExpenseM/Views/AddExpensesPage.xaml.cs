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

    private int totalRowsCount = 1;
    private const int MaxRowsCount = 12;
    Button saveBtn = new Button();

    List<ExpensesAddRow> labeledTextboxes = new List<ExpensesAddRow>();
    public AddExpensesPage()
    {
      InitializeComponent();
      //create user control here and after creating it, move it to the user control file(expense add)
      Thickness thickness = new Thickness();
      thickness.Top = 10;
      saveBtn.Content = "Save Records";
      saveBtn.HorizontalAlignment = HorizontalAlignment.Center;
      saveBtn.Margin = thickness;
      saveBtn.Click += SaveBtn_Click;
      this.AddRows();

    }
    private void AddRowsBtn_Click(object sender, RoutedEventArgs e)
    {
      this.AddRows();
    }

    private void AddRows()
    {
      int rowsCount = int.Parse(this.RowCountBox.Text);
      Thickness thickness = new Thickness();
      thickness.Top = 10;
      int tempRowCount = rowsCount + totalRowsCount;

      if (rowsCount <= MaxRowsCount && tempRowCount <= MaxRowsCount)
      {
        totalRowsCount += rowsCount;
        for (int i = 0; i < rowsCount; i++)
        {
          ExpensesAddRow expensesAddRow = new ExpensesAddRow();

          expensesAddRow.Margin = thickness;
          this.MainPannel.Children.Add(expensesAddRow);
        }

        if (this.MainPannel.Children.Contains(saveBtn))
        {

          this.MainPannel.Children.Remove(saveBtn);

        }

        this.MainPannel.Children.Add(saveBtn);

      }
      else
      {
        MessageBox.Show(String.Format(
          Properties.Resources.MAXIMUM_RECORDS_MESSAGE,
          MaxRowsCount));
      }
    }

    private void SaveBtn_Click(object sender, RoutedEventArgs e)
    {

      
    }
  }
}
