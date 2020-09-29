using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace phonebook
{
    public partial class frmInput : Form
    {
        public frmInput()
        {
            InitializeComponent();
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void SetDefaults()
        {
            txtMobileNo.ForeColor = Color.Gray;
            txtFirstName.ForeColor = Color.Gray;
            txtMiddleName.ForeColor = Color.Gray;
            txtLastName.ForeColor = Color.Gray;
            txtEmail.ForeColor = Color.Gray;
            comboBox1.ForeColor = Color.Gray;
            txtMobileNo.Text = "Contact No.";
            txtFirstName.Text = "First Name";
            txtMiddleName.Text = "Middle Name";
            txtLastName.Text = "Last Name";
            txtEmail.Text = "Email";
            comboBox1.SelectedIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            if (txtMobileNo.Text != "Contact No.")
            {
                txtMobileNo.ForeColor =Color.Black;
            }
            
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (txtFirstName.Text != "First Name")
            {
                txtFirstName.ForeColor = Color.Black;
            }

        }

        private void txtMiddleName_TextChanged(object sender, EventArgs e)
        {
            if (txtMiddleName.Text != "Middle Name")
            {
                txtMiddleName.ForeColor = Color.Black;
            }
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            if (txtLastName.Text != "Last Name")
            {
                txtLastName.ForeColor = Color.Black;
            }

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text != "Email")
            {
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int genderID;
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
                Contacts contact = new Contacts();
                contact.FirstName = txtFirstName.Text;
                contact.MiddleName = txtMiddleName.Text;
                contact.LastName = txtLastName.Text;
                contact.Email = txtEmail.Text;
                contact.DOB = dtpDOB.Value;
                contact.GenderID = genderID;
                contact.Group = comboBox1.SelectedItem.ToString();
                contact.PhoneNo = txtMobileNo.Text;
                contact.InsertData();
                SetDefaults();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

}




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                comboBox1.ForeColor = Color.Black;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmData fd = new frmData();
            fd.Show();
            this.Hide();
        }
    }
}
