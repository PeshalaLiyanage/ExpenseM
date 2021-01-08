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
  class TransactionModel
  {

    public UserModel Contact { get; set; }
    public int Amount { get; set; }
    public int TransactionType { get; set; }
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

    public TransactionModel(UserModel contact, int amount, int transactionType, int recurrentStatus, string description, DateTime startDate, dynamic endDate)
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
            element.EndDate == null ? Convert.ToDateTime("0001-01-01") : element.EndDate,
            element.Contact);

          Transaction transaction = new Transaction();
          transaction.Amount = (short)element.Amount;
          transaction.TransactionTyoe = (short)element.TransactionType;
          transaction.RecurrentStatus = (short)element.RecurrentStatus;
          transaction.StartDate = element.StartDate;
          transaction.EndDate = null;
          transaction.CreatedAt = DateTime.Now;
          transaction.Description = element.Description;
          transaction.UserId = element.Contact.UserId;
          DBConnection.Connection.Transactions.Add(transaction);
        }

        FileUtilities.GetInstance.WriteToFile(tempTransactionData, Properties.Resources.PATH_TRANSACTION_TEMP_DATA);



        DBConnection.Connection.SaveChanges();

        return true;

      }

      catch (Exception ex)
      {
        tempTransactionData.ReadXml(Properties.Resources.PATH_TRANSACTION_TEMP_DATA);
        //ExpenseMDataSet.UserRow userData = tempTransactionData.User[0];
        ExpenseMDataSet.TransactionDataTable transactionRows = tempTransactionData.Transaction;

        List<TransactionModel> transactionModels = new List<TransactionModel>();

        // TODO - retrying and re populating
        foreach (ExpenseMDataSet.TransactionRow item in transactionRows)
        {
          //transactionModels.Add(new TransactionModel(
          //  new UserModel(item.Contact.FirstName, item.Contact.LastName, item.Contact.Address,item.Contact.PhoneNumber,item.Contact.Email,item.Contact.UserType),
          //  item.Amount,
          //  item.TransactionType,
          //  item.RecurrentStatus,
          //  item.Description,
          //  item.StartDate,
          //  item.EndDate
          //  ));
        }

        return false;
      }




    }
  }
}
