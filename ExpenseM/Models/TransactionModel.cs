using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseM.Utilities;
using ExpenseM.Models;
using ExpenseM.Entities;

namespace ExpenseM.Models
{
  public class TransactionModel
  {
    public UserModel Contact { get; set; }
    public int Amount { get; set; }
    public EnumTransactionType TransactionType { get; set; }
    public int RecurrentStatus { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public dynamic EndDate { get; set; }
    private DateTime createdAt = DateTime.Now;

    public DateTime CreatedAt
    {
      get { return createdAt; }
      set { createdAt = value; }
    }

    ExpenseMDataSet tempTransactionData = new ExpenseMDataSet();

    public TransactionModel()
    {
    }

    public TransactionModel(
      UserModel contact,
      int amount,
      EnumTransactionType transactionType,
      int recurrentStatus,
      string description,
      DateTime startDate,
      dynamic endDate)
    {
      Contact = contact;
      Amount = amount;
      TransactionType = transactionType;
      RecurrentStatus = recurrentStatus;
      Description = description;
      StartDate = startDate;
      EndDate = endDate;
    }

    public bool SaveTransactions(List<TransactionModel> transactions = null)
    {
      try
      {
        foreach (TransactionModel element in transactions)
        {
          tempTransactionData.Transaction.AddTransactionRow(
            element.Amount,
            (short)element.TransactionType,
            (short)element.RecurrentStatus,
            element.Description,
            element.StartDate,
            element.EndDate == null ? default(DateTime) : element.EndDate,
            element.Contact);

          Transaction transaction = new Transaction();
          transaction.Amount = (short)element.Amount;
          transaction.TransactionTyoe = (short)element.TransactionType;
          transaction.RecurrentStatus = (short)element.RecurrentStatus;
          transaction.StartDate = element.StartDate;
          transaction.EndDate = element.EndDate;
          transaction.CreatedAt = DateTime.Now;
          transaction.Description = element.Description;
          transaction.UserId = element.Contact.UserId;
          DBConnection.Connection.Transactions.Add(transaction);
        }

        DBConnection.Connection.SaveChanges();
        return true;

      }

      catch (Exception)
      {
        tempTransactionData.ReadXml(Properties.Resources.PATH_TRANSACTION_TEMP_DATA);
        ExpenseMDataSet.TransactionDataTable transactionRows = tempTransactionData.Transaction;
        return false;
      }
    }

    // Get transaction functionality
    public List<TransactionModel> getTransactions(
      DateTime fromDate = default(DateTime),
      DateTime toDate = default(DateTime),
      bool recurring = false,
      bool futureRecurring = false)
    {
      try
      {
        List<TransactionModel> transactionList = new List<TransactionModel>();

        // Fetch transactions according to the parameters
        dynamic records =
          fromDate != default(DateTime)
          && toDate != default(DateTime)
          && recurring == true
        ? DBConnection.Connection.Transactions.Where(
          transaction => transaction.StartDate >= fromDate
          && transaction.StartDate <= toDate
          && transaction.RecurrentStatus == 1
          ).ToList()
        : fromDate != default(DateTime)
          && toDate != default(DateTime)
        ? DBConnection.Connection.Transactions.Where(
          transaction => transaction.StartDate >= fromDate
          && transaction.StartDate <= toDate
        ).ToList()
        : fromDate != default(DateTime)
          && recurring == true
        ? DBConnection.Connection.Transactions.Where(
          transaction => transaction.StartDate >= fromDate
          && transaction.RecurrentStatus == 1
        ).ToList()
        : fromDate != default(DateTime)
        ? DBConnection.Connection.Transactions.Where(
          transaction => transaction.StartDate >= fromDate
        ).ToList()
        : futureRecurring == true   // Fetch records for financial status calculation
          && toDate != default(DateTime)
          && recurring == true
        ? DBConnection.Connection.Transactions.Where(
          transaction => transaction.EndDate >= toDate
          && transaction.RecurrentStatus == 1
        ).ToList()
        : toDate != default(DateTime)
          && recurring == true
        ? DBConnection.Connection.Transactions.Where(
          transaction => transaction.StartDate <= toDate
          && transaction.RecurrentStatus == 1
        ).ToList()
        : toDate != default(DateTime)
        ? DBConnection.Connection.Transactions.Where(
          transaction => transaction.StartDate <= toDate
        ).ToList()
        : recurring == true ?
          DBConnection.Connection.Transactions.Where(
          transaction => transaction.RecurrentStatus == 1
        ).ToList()
        : DBConnection.Connection.Transactions.ToList();

        foreach (Transaction item in records)
        {
          UserModel contact = new UserModel();

          transactionList.Add(new TransactionModel(
            contact.GetUserById(item.UserId),
            item.Amount,
            (EnumTransactionType)item.TransactionTyoe,
            item.RecurrentStatus,
            item.Description,
            item.StartDate,
            item.EndDate
            ));
        }

        return transactionList;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
