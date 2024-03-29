﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExpenseM.Entities;
using ExpenseM.Utilities;
using ExpenseM.Views;

namespace ExpenseM.Models
{
  public class UserModel
  {
    private String firstName;
    private String lastName;
    private String address;
    private String phoneNumber;
    private String email;
    private int userType;
    private String password = null;

    private int _retryCount = 0;

    ExpenseMDataSet tempUserData = new ExpenseMDataSet();

    public UserModel()
    {
    }

    public UserModel(string firstName, string lastName, string address, string phoneNumber, string email, int userType)
    {
      this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
      this.lastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
      this.address = address ?? throw new ArgumentNullException(nameof(address));
      this.phoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
      this.email = email ?? throw new ArgumentNullException(nameof(email));
      this.userType = userType;
    }

    public int UserId { get; set; }

    public String Password
    {
      get { return password; }
      set { password = value; }
    }

    public int UserType
    {
      get { return userType; }
      set { userType = value; }
    }

    public String Email
    {
      get { return email; }
      set { email = value; }
    }

    public String PhoneNumber
    {
      get { return phoneNumber; }
      set { phoneNumber = value; }
    }

    public String Address
    {
      get { return address; }
      set { address = value; }
    }

    public String LastName
    {
      get { return lastName; }
      set { lastName = value; }
    }

    public String FirstName
    {
      get { return firstName; }
      set { firstName = value; }
    }

    // save user data in database
    public void AddUser()
    {
      String filePath = userType == 1
          ? Properties.Resources.PATH_ADMIN_USER_DATA
          : Properties.Resources.PATH_CONTACT_USER_DATA;
      try
      {
        tempUserData.User.AddUserRow(firstName, lastName, address, phoneNumber, email, password);
        FileUtilities.GetInstance.WriteToFile(tempUserData, filePath);

        User user = new User();

        user.FirstName = firstName;
        user.LastName = lastName;
        user.Address = address;
        user.PhoneNo = phoneNumber;
        user.Email = email;
        user.Password = password;
        user.UserType = (short)userType;
        user.CreatedAt = DateTime.Now;

        DBConnection.Connection.Users.Add(user);
        DBConnection.Connection.SaveChanges();

      }
      catch (Exception)
      {
        // retry to save data by reading data from file if the saving process failed
        if (_retryCount < 10)
        {
          tempUserData.ReadXml(filePath);
          ExpenseMDataSet.UserRow userData = tempUserData.User[0];

          firstName = userData.FirstName;
          lastName = userData.LastName;
          address = userData.Address;
          phoneNumber = userData.PhoneNumber;
          email = userData.Email;
          password = userData.Password;

          _retryCount++;
          AddUser();
        }
        else
        {
          throw new Exception("User " + firstName + " saving failed");
        }
      }
      finally
      {
        FileUtilities.GetInstance.DeleteFile(filePath);
      }
    }

    // Get cahced user data from file
    public UserModel getTempUserDataFromFile()
    {
      if (File.Exists(Properties.Resources.PATH_USER_TEMP_DATA) == true)
      {
        tempUserData.ReadXml(Properties.Resources.PATH_USER_TEMP_DATA);
        ExpenseMDataSet.UserRow userData = tempUserData.User[0];

        return new UserModel(
            userData.FirstName,
            userData.LastName,
            userData.Address,
            userData.PhoneNumber,
            userData.Email, 1);
      }
      return null;
    }

    // Get cahced contacts data from file
    public UserModel getTempContactDataFromFile()
    {
      if (File.Exists(Properties.Resources.PATH_CONTACT_USER_DATA) == true)
      {
        tempUserData.ReadXml(Properties.Resources.PATH_CONTACT_USER_DATA);
        ExpenseMDataSet.UserRow userData = tempUserData.User[0];

        return new UserModel(
            userData.FirstName,
            userData.LastName,
            userData.Address,
            userData.PhoneNumber,
            userData.Email, 1);
      }
      return null;
    }

    // Write cache files
    public void AddTempUserDataToFile(string path)
    {
      Task.Run(() =>
      {
        tempUserData.User.Clear();
        tempUserData.User.AddUserRow(firstName, lastName, address, phoneNumber, email, password);
        FileUtilities.GetInstance.DeleteFile(path);
        FileUtilities.GetInstance.WriteToFile(tempUserData, path);
      });

    }

    public bool ValidateUsernamePassword(String username, String password)
    {
      try
      {
        dynamic record = DBConnection.Connection.Users.Where(
            user => user.Email == username
            && user.Password == password
            ).FirstOrDefault();

        return record != null;
      }
      catch (Exception)
      {
        throw new Exception("Something went wrong");
      }
    }

    // Fetch users
    public List<UserModel> FetchUsers(int type)
    {
      try
      {
        List<UserModel> users = new List<UserModel>();

        List<User> records = DBConnection.Connection.Users.Where(
            user => user.UserType == type
            ).ToList<User>();

        foreach (User item in records)
        {
          UserModel user = new UserModel(
            item.FirstName,
            item.LastName,
            item.Address,
            item.PhoneNo,
            item.Email,
            item.UserType
            );

          user.UserId = item.Id;
          users.Add(user);
        }

        return users;
      }
      catch (Exception)
      {
        throw new Exception("Something went wrong");
      }
    }

    public override string ToString()
    {
      return this.FirstName;
    }

    public UserModel GetUserById(int userId)
    {
      try
      {
        User record = DBConnection.Connection.Users.Where(
            user => user.Id == userId
            ).FirstOrDefault();

        UserModel userModel = new UserModel(
          record.FirstName,
          record.LastName,
          record.Address,
          record.PhoneNo,
          record.Email,
          record.UserType
          );

        return userModel;
      }
      catch (Exception)
      {
        throw new Exception("Something went wrong");
      }
    }
  }
}
