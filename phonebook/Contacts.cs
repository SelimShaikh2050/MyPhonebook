using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace phonebook
{
    public class Contacts
    {
        Connection c = new Connection();
        private int _genderID;
        
        private string _firstName;
        private string _mName;
        private string _lName;
        private string _email;
        private string _phNo;
        private DateTime _DOB;
        
        private string _group;
        public int GenderID 
        {
            get=>_genderID;
            set
            {
                if (value==1||value==2)
                {
                    _genderID = value;
                }
                else
                {
                    throw new NoNullAllowedException();
                }
            }
        }
        public DateTime DOB
        {
            get;set;
        }


        
        public string FirstName
        { 
            get=> _firstName;
            set
            {
                if (value == "First Name" || value == "")
                {
                    MessageBox.Show("You must provide First Name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new NoNullAllowedException();
                }
                else
                {
                    _firstName = "'" + value + "'";
                }
            } 
        }
        public string MiddleName
        { 
            get=>_mName;
            set
            {
                if (value == "Middle Name" || value == "")
                {
                    _mName = "Null";
                }
                else
                {
                    _mName = "'" + value + "'";
                }
            } 
        }
        public string LastName 
        { 
            get=>_lName;
            set
            {
                if (value == "Last Name" || value == "")
                {
                    _lName = "Null";
                }
                else
                {
                    _lName = "'" + value + "'";
                }
            } 
        }
        public string Email 
        { 
            get=>_email;
            set
            {
                if (value == "Email" || value == "")
                {
                    _email = "Null";
                }
                else
                {
                    _email = "'" + value + "'";
                }
            }
        }
        public string PhoneNo 
        { 
            get=>_phNo;
            set
            {
                if (value == "Contact No." || value == "")
                {
                    MessageBox.Show("You must provide Contact No.!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new NoNullAllowedException();
                }
                else
                {
                    _phNo = value ;
                }
            }
        }

        public string Group /*{ get; set; }*/
        {
            get=>_group;
            set
            {
                if (value == "--Select Group--")
                {
                    _group = "Null";
                }
                else
                {
                    _group = "'" + value + "'";
                }
            }
        }

        public string OldPhoneNo { get;  set; }

        public Contacts()
        {

        }

        public Contacts(string firstName, string middleName, string lastName, int genderID, string email, string phoneNo, DateTime dob, string group)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            GenderID = genderID;
            Email = email;
            PhoneNo = phoneNo;
            _DOB = dob;
            Group = group;
        }
        public Contacts(string firstName, string middleName, string lastName, int genderID, string email, string phoneNo, DateTime dob, string group,string oldP)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            GenderID = genderID;
            Email = email;
            PhoneNo = phoneNo;
            _DOB = dob;
            Group = group;
            OldPhoneNo = oldP;
        }

        public void InsertData()
        {
            using (SqlConnection conn = c.Con)
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO tblMain VALUES ( " + FirstName + "," + MiddleName + "," + LastName + "," + Group + "," + GenderID + ",'" + PhoneNo + "'," + Email + ",'" + DOB + "')";
                _ = cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Contact Saved successfully!!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void UpdateData()
        {
            using (SqlConnection conn = c.Con)
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE tblMain SET [firstName] = " + FirstName + " ,[middleName] = " + MiddleName + " ,[lastName] = " + LastName + " ,[groups] = " + Group + " ,[genderID] = " + GenderID + " ,[mobileNo] = '" + PhoneNo + "' ,[email] = " + Email + " ,[dateOfBirth] = '" + DOB + "' WHERE [mobileNo] = " + OldPhoneNo;
                    _ = cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Contact updated successfully!!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
