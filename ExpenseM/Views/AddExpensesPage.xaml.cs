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
using ExpenseM.Models;
using ExpenseM.Utilities;

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

    List<ExpensesAddRow> expensesAddRows = new List<ExpensesAddRow>();


    public AddExpensesPage()
    {
      InitializeComponent();
      this.WindowTitle = "Add Transactions";
      Thickness thickness = new Thickness();
      thickness.Top = 10;
      saveBtn.Content = "Save Records";
      saveBtn.HorizontalAlignment = HorizontalAlignment.Center;
      saveBtn.Margin = thickness;
      saveBtn.Width = 200;
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
          expensesAddRow.SelectedStartDate = DateTime.Now;
          expensesAddRows.Add(expensesAddRow);
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

    List<TransactionModel> transactionsList;

    private async void SaveBtn_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        transactionsList = new List<TransactionModel>();
        foreach (ExpensesAddRow row in expensesAddRows)
        {
          transactionsList.Add(new TransactionModel(
            row.SelectedContact,
            int.Parse(row.Amount),
            (EnumTransactionType)row.SelectedTransactionType.Key,
            row.SelectedEndDate == null ? 0 : 1,
            row.Description,
            row.SelectedStartDate,
            row.SelectedEndDate
            ));
        }
        TransactionModel transactionModel = new TransactionModel();
        bool result = await Task.Run(() => transactionModel.SaveTransactions(transactionsList));


        if (result)
        {
          MessageBox.Show("Saved");
        }
        else
        {
          MessageBox.Show("Failed");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
  }
}
