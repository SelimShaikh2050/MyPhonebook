using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace phonebook
{
    public partial class frmContacts : Form
    {
        private string fName;
        private string mName;
        private string lName;
        private string contactNo;
        private string email;
        private DateTime dob;
        private string gender;
        private string group;
        private string oldphoneNo;

        public frmContacts()
        {
            InitializeComponent();
        }
        public frmContacts(string fName, string mName, string lName, string contactNo, string email, DateTime dob, string gender, string group)
        {
            InitializeComponent();
            this.fName = fName;
            this.mName = mName;
            this.lName = lName;
            this.contactNo = contactNo;
            this.oldphoneNo = contactNo;
            this.email = email;
            this.dob = dob;
            this.gender = gender;
            this.group = group;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmData fData = new frmData();
            fData.Show();
            this.Hide();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            GC.Collect();
            this.Close();
        }

        private void frmContacts_Load(object sender, EventArgs e)
        {
            txtFirstName.Text = fName;
            txtMobileNo.Text = contactNo;
            if (lName != "")
            {
                txtLastName.Text = lName;
            }
            else
            {
                txtLastName.Text = "Last Name";
            }
            if (mName != "")
            {
                txtMiddleName.Text = mName;
            }
            else
            {
                txtMiddleName.Text = "Middle Name";
            }

            if (email != "")
            {
                txtEmail.Text = email;
            }
            else
            {
                txtEmail.Text = "Email";
            }

            if (group != "")
            {
                cmbGroup.SelectedText = group;
            }
            if (dob != null)
            {
                dtpDOB.Value = dob;
            }

            if (gender == "Male")
            {
                rdMale.Checked = true;
            }
            else
            {
                rdFemale.Checked = true;
            }
        }

        private void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            txtChangedOptions("Contact No", txtMobileNo);
        }


        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            txtChangedOptions("First Name", txtFirstName);
        }

        private void txtMiddleName_TextChanged(object sender, EventArgs e)
        {
            txtChangedOptions("Middle Name", txtMiddleName);

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            txtChangedOptions("Last Name", txtLastName);

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtChangedOptions("Email", txtEmail);

        }

        private void txtChangedOptions(string defaultValue, TextBox textBox)
        {
            string defaultV = defaultValue;
            int defLen = defaultV.Length;
            TextBox t = textBox;
            string test = t.Text;
            bool endsWith = test.EndsWith(defaultV);
            bool startWith = test.StartsWith(defaultV);
            int leng = test.Length;
            if (test == "")
            {
                t.Text = defaultV;
            }

            if (test == defaultV)
            {
                t.ForeColor = Color.Gray;
            }
            else
            {
                t.ForeColor = Color.Black;
            }

            if (endsWith && leng > defLen)
            {
                t.Text = test.Replace(defaultV, "");
            }
            if (startWith && leng > defLen)
            {
                t.Text = test.Replace(defaultV, "");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int genderID;
                string groupp;
                if (rdMale.Checked)
                {
                    genderID = 1;
                }
                else if (rdFemale.Checked)
                {
                    genderID = 2;
                }
                else
                {
                    MessageBox.Show("Please Provide Gender.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new NoNullAllowedException();
                }

                if (cmbGroup.SelectedItem != null)
                {
                    groupp = cmbGroup.SelectedItem.ToString();
                }
                else
                {
                    groupp = group;
                }
                Contacts contact = new Contacts();
                contact.FirstName = txtFirstName.Text;
                contact.MiddleName = txtMiddleName.Text;
                contact.LastName = txtLastName.Text;
                contact.Email = txtEmail.Text;
                contact.DOB = dtpDOB.Value;
                contact.GenderID = genderID;
                contact.Group = groupp;
                contact.PhoneNo = txtMobileNo.Text;
                contact.OldPhoneNo = oldphoneNo;
                contact.UpdateData();
                frmData fw = new frmData();
                fw.Show();
                this.Hide();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
