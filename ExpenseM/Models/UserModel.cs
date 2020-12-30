using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseM.Entities;
using ExpenseM.Utilities;

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

        public bool AddUser()
        {

            tempUserData.User.AddUserRow(firstName, lastName, address, phoneNumber, email, password);
           
            tempUserData.WriteXml(@"C:\Users\pesha\ExpenseM\AdminUserData.xml");

            User adminUser = new User();
            adminUser.FirstName = 


            DBConnection.GetInstance.DB.Users.Add();

            return true;
        }

        public UserModel getTempUserDataFromFile()
        {
            if (File.Exists(@"C:\Users\pesha\ExpenseM\UserTempData.xml") == true)
            {
                tempUserData.ReadXml(@"C:\Users\pesha\ExpenseM\UserTempData.xml");
                ExpenseMDataSet.UserRow userData = tempUserData.User[0];
                Console.WriteLine("this is temp data : "+ userData.FirstName);
                return new UserModel(
                    userData.FirstName,
                    userData.LastName,
                    userData.Address,
                    userData.PhoneNumber,
                    userData.Email, 1);
            }
            return null;
        }

        public bool AddTempUserDataToFile()
        {
            // write a thread for this
            tempUserData.User.Clear();
            tempUserData.User.AddUserRow(firstName, lastName, address, phoneNumber, email, password);
            CommonUtilities.GetInstance.DeleteFile(@"C:\Users\pesha\ExpenseM\UserTempData.xml");
            CommonUtilities.GetInstance.WriteToFile(tempUserData, @"C:\Users\pesha\ExpenseM\UserTempData.xml");



            //DBConnection.GetInstance.DB.
            

            //tempUserData.WriteXml(@"C:\Users\pesha\ExpenseM\UserTempData.xml");
            //tempUserData.User.Clear();

            return true;
        }
    }
}
