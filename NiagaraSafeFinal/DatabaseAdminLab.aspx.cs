using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Windows;
using NiagaraSafeFinal;

namespace SafetyAuth
{
    public partial class DatabaseAdminLab : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Globals.role == "Admin")
            {
                if (!IsPostBack)
                {
                    LoadData();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void LoadData()
        {
            //Get the inspectionID
            int labID = Convert.ToInt32(Request.QueryString["labID"]);

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Lab WHERE ID=@labID";
            comm.Parameters.AddWithValue("@labID", labID);

            //Open the connection
            conn.Open();

            //Execute the command
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Fill the textboxes
                txtLabID.Text = reader.GetInt32(0).ToString();
                txtLabName.Text = reader.GetString(1);

                string school = reader.GetString(2);

                if (school == "Media")
                {
                    ddlSchool.Items.Add("Media");
                    ddlSchool.Items.Add("Trades");
                    ddlSchool.Items.Add("Technology");
                }

                if (school == "Trades")
                {
                    ddlSchool.Items.Add("Trades");
                    ddlSchool.Items.Add("Media");
                    ddlSchool.Items.Add("Technology");
                }

                if (school == "Technology")
                {
                    ddlSchool.Items.Add("Technology");
                    ddlSchool.Items.Add("Trades");
                    ddlSchool.Items.Add("Media");
                }

            }

            //Close the connection
            conn.Close();

            lblName1.Text = Globals.firstName + " " + Globals.lastName;
            lblName2.Text = Globals.firstName + " " + Globals.lastName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Are you sure you want to save this technician?";
            string caption = "Technician Edit Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the variables from the form
                int labID = Convert.ToInt32(txtLabID.Text);
                string labName = txtLabName.Text;
                string school = ddlSchool.SelectedItem.Text;

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "UPDATE Lab SET Title=@labName, School=@school WHERE ID= " + labID;
                comm.Parameters.AddWithValue("@labName", labName);
                comm.Parameters.AddWithValue("@school", school);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Lab Updated.");
                }
                else
                {
                    MessageBox.Show("Error.  No Action Taken.");
                }
            }
            if (result == MessageBoxResult.No | result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("No Action Taken");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Allow the user to back out
            string messageBoxText = "Are you sure you want to delete this lab?";
            string caption = "Lab Technician Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the lab ID
                int labID = Convert.ToInt32(txtLabID.Text);
                
                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "DELETE FROM Lab WHERE ID = @labID";
                comm.Parameters.AddWithValue("@labID", labID);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Lab Deleted.");
                }
                else
                {
                    MessageBox.Show("Error.  No Action Taken.");
                }
            }
            if (result == MessageBoxResult.No | result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("No Action Taken");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Redirect back to the admin page
            Response.Redirect("DatabaseAdmin.aspx");
        }
    }
}