using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentGrades
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            populateStudentInfo();
        }

        public void populateStudentInfo()
        {
            gvAssignmentInfo.Visible = false;
            label2.Visible = false;
            lblWarning.Visible = false;
            DataTable dataToReturn = new DataTable();
            string connectionStr = "Server=localhost; Database=Student; Integrated Security=True;";
            try
            {
                SqlConnection connection = new SqlConnection(connectionStr);

                SqlCommand cmd = new SqlCommand("prGetStudentInfo", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataToReturn);
                gvStudentInfo.DataSource = dataToReturn;
            }
            catch (Exception ex)
            {

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String studentName = txtName.Text.ToString();
            DataTable dataToReturn_AssignmentInfo = new DataTable();
            string connectionStr = "Server=localhost; Database=Student; Integrated Security=True;";
            try
            {
                SqlConnection connection = new SqlConnection(connectionStr);

                SqlCommand cmd = new SqlCommand("prSearchStudentAssignmentWiseInfo", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StudentName", studentName));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataToReturn_AssignmentInfo);

                if (dataToReturn_AssignmentInfo.Rows.Count > 0)
                {
                    gvAssignmentInfo.DataSource = dataToReturn_AssignmentInfo;
                    gvAssignmentInfo.Visible = true;
                    label2.Visible = true;
                    lblWarning.Visible = false;
                }
                else
                {
                    lblWarning.Visible = true;
                    gvAssignmentInfo.Visible = false;
                    label2.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
