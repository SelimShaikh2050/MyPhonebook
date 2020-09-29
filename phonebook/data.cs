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
    public partial class frmData : Form
    {
        
        public frmData()
        {
            InitializeComponent();
        }

        private void data_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbSearchBy.SelectedIndex = 0;

        }

        private void LoadData()
        {
            Connection c = new Connection();
            using (SqlConnection con = c.Con)
            {
                string comdtext = "SET CONCAT_NULL_YIELDS_NULL OFF SELECT M.firstName + ' ' + M.middleName + ' ' + M.lastName AS[NAME],M.mobileNo AS CONTACT,M.email EMAIL, M.dateOfBirth DOB, G.gender GENDER, M.groups AS[GROUP]  FROM tblMain M JOIN tblGender G ON M.genderID = G.genderID SET CONCAT_NULL_YIELDS_NULL ON";
                SqlDataAdapter sda = new SqlDataAdapter(comdtext, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                ds.Dispose();
                sda.Dispose();
            }

        }

        private void btnEntryForm_Click(object sender, EventArgs e)
        {
            frmInput fin = new frmInput();
            fin.Show();
            this.Hide();
        }



        private void SearchContacts()
        {
            Connection c = new Connection();
            using (SqlConnection con = c.Con)
            {
                string comdtext = "SET CONCAT_NULL_YIELDS_NULL OFF SELECT M.firstName + ' ' + M.middleName + ' ' + M.lastName AS[NAME],M.mobileNo AS CONTACT,M.email EMAIL, M.dateOfBirth DOB, G.gender GENDER, M.groups AS[GROUP]  FROM tblMain M JOIN tblGender G ON M.genderID = G.genderID where M." + cmbSearchBy.SelectedItem + " like '" + txtSearch.Text + "%' SET CONCAT_NULL_YIELDS_NULL ON";
                SqlDataAdapter sda = new SqlDataAdapter(comdtext, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                ds.Dispose();
                sda.Dispose();
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchContacts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                string contactno = row.Cells[1].Value.ToString();
                Connection c = new Connection();
                using (SqlConnection con = c.Con)
                {
                    string comdtext = "SELECT M.firstName , M.middleName,M.lastName  ,M.mobileNo,M.email, M.dateOfBirth, G.gender, M.groups FROM tblMain M JOIN tblGender G ON M.genderID = G.genderID where M.mobileNo='" + contactno + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(comdtext, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        string firstName = ds.Tables[0].Rows[0][0].ToString();
                        string middleName = ds.Tables[0].Rows[0][1].ToString();
                        string lastName = ds.Tables[0].Rows[0][2].ToString();
                        string contactNo = ds.Tables[0].Rows[0][3].ToString();
                        string email = ds.Tables[0].Rows[0][4].ToString();
                        DateTime dob = Convert.ToDateTime(ds.Tables[0].Rows[0][5]);
                        string gender = ds.Tables[0].Rows[0][6].ToString();
                        string group = ds.Tables[0].Rows[0][7].ToString();
                        frmContacts cont = new frmContacts(firstName, middleName, lastName, contactNo, email, dob, gender, group);
                        cont.Show();
                        this.Hide();
                    }
                }
            }
            else if (dataGridView1.SelectedRows.Count == 0)
            {
                Connection c = new Connection();
                using (SqlConnection con = c.Con)
                {
                    string comdtext = "SELECT M.firstName , M.middleName,M.lastName  ,M.mobileNo,M.email, M.dateOfBirth, G.gender, M.groups FROM tblMain M JOIN tblGender G ON M.genderID = G.genderID where M." + cmbSearchBy.SelectedItem + " like '" + txtSearch.Text + "%' ";
                    SqlDataAdapter sda = new SqlDataAdapter(comdtext, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        string firstName = ds.Tables[0].Rows[0][0].ToString();
                        string middleName = ds.Tables[0].Rows[0][1].ToString();
                        string lastName = ds.Tables[0].Rows[0][2].ToString();
                        string contactNo = ds.Tables[0].Rows[0][3].ToString();
                        string email = ds.Tables[0].Rows[0][4].ToString();
                        DateTime dob = Convert.ToDateTime(ds.Tables[0].Rows[0][5]);
                        string gender = ds.Tables[0].Rows[0][6].ToString();
                        string group = ds.Tables[0].Rows[0][7].ToString();
                        frmContacts cont = new frmContacts(firstName, middleName, lastName, contactNo, email, dob, gender, group);
                        cont.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("You did't select any contacts!!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                Connection cc = new Connection();
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                string contactno = row.Cells[1].Value.ToString();
                DialogResult res = MessageBox.Show("Are you sure to erase the contact?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    using (SqlConnection con = cc.Con)
                    {
                        string cmdtext = "DELETE tblMain WHERE mobileNo='" + contactno + "'";
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = cmdtext;
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Contact erased successfully!!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("None of Contacts are erased!!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else if (dataGridView1.SelectedRows.Count > 1)
            {
                DialogResult res = MessageBox.Show("Are you sure to erase the contacts?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        DataGridViewRow row = this.dataGridView1.SelectedRows[i];
                        string contactno = row.Cells[1].Value.ToString();

                        Connection c = new Connection();
                        using (SqlConnection con = c.Con)
                        {
                            string cmdtext = "DELETE tblMain WHERE mobileNo='" + contactno + "'";
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.CommandText = cmdtext;
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }

                    MessageBox.Show("Contacts erased successfully!!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("None of Contacts are erased!!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select contact to erase", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadData();
        }
    }
}
