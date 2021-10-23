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
    List<TransactionModel> transactionsList;

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
      saveBtn.Click += SaveBtn_Click; // link button click functionality through delegate functionality
      this.AddRows();
    }
    private void AddRowsBtn_Click(object sender, RoutedEventArgs e)
    {
      this.AddRows();
    }

    // Dynamically adding rows
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
          ExpensesAddRow expensesAddRow = new ExpensesAddRow(); // create user controller rows

          expensesAddRow.Margin = thickness;
          expensesAddRow.SelectedStartDate = DateTime.Now;
          expensesAddRows.Add(expensesAddRow);
          this.MainPannel.Children.Add(expensesAddRow); // add rows to the stack pannel
        }

        if (this.MainPannel.Children.Contains(saveBtn))
        {
          this.MainPannel.Children.Remove(saveBtn); // remove save button duplications
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

    private async void SaveBtn_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        transactionsList = new List<TransactionModel>();
        foreach (ExpensesAddRow row in expensesAddRows)
        {
          // create transaction objects
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

        // run the transactions save functionality in a different thread
        bool result = await Task.Run(() => transactionModel.SaveTransactions(transactionsList));


        if (result)
        {
          MessageBox.Show(Properties.Resources.DATA_SAVE_SUCCESS, this.Title);
        }
        else
        {
          MessageBox.Show(Properties.Resources.SOMETHING_WRONG);
        }
      }
      catch (Exception)
      {
        MessageBox.Show(Properties.Resources.SOMETHING_WRONG);
      }
    }
  }
}
