using System;
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
    class UserModel
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

        public void AddUser()
        {
            try
            {
                tempUserData.User.AddUserRow(firstName, lastName, address, phoneNumber, email, password);
                FileUtilities.GetInstance.WriteToFile(tempUserData, Properties.Resources.PATH_ADMIN_USER_DATA);

                User adminUser = new User();

                adminUser.FirstName = firstName;
                adminUser.LastName = lastName;
                adminUser.Address = address;
                adminUser.PhoneNo = phoneNumber;
                adminUser.Email = email;
                adminUser.Password = password;
                adminUser.UserType = 1; // use an enum for this
                adminUser.CreatedAt = DateTime.Now;

                DBConnection.Connection.Users.Add(adminUser);
                DBConnection.Connection.SaveChanges();

            }
            catch (Exception e)
            {
                if (_retryCount < 10)
                {
                    tempUserData.ReadXml(Properties.Resources.PATH_ADMIN_USER_DATA);
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
                FileUtilities.GetInstance.DeleteFile(Properties.Resources.PATH_ADMIN_USER_DATA);
            }
        }

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

        public void AddTempUserDataToFile()
        {
            Task.Run(() =>
            {
                tempUserData.User.Clear();
                tempUserData.User.AddUserRow(firstName, lastName, address, phoneNumber, email, password);
                FileUtilities.GetInstance.DeleteFile(Properties.Resources.PATH_USER_TEMP_DATA);
                FileUtilities.GetInstance.WriteToFile(tempUserData, Properties.Resources.PATH_USER_TEMP_DATA);
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
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}
