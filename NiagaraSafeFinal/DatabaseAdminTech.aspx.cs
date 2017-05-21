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
    public partial class DatabaseAdminTech : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Globals.role == "Admin")
            //{
            //    if (!IsPostBack)
            //    {
            //        LoadData();
            //    }
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            //Get the inspectionID
            int technicianID = Convert.ToInt32(Request.QueryString["technicianID"]);

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Technician WHERE ID=@technicianID";
            comm.Parameters.AddWithValue("@technicianID", technicianID);

            //Open the connection
            conn.Open();

            //Execute the command
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Fill the textboxes
                txtTechnicianID.Text = reader.GetInt32(0).ToString();
                txtTechnicianFirst.Text = reader.GetString(1);
                txtTechnicianLast.Text = reader.GetString(2);
                txtTechnicianPassword.Text = reader.GetString(4);
                txtTechnicianEmail.Text = reader.GetString(5);
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
                //Get the inspectionID
                int technicianID = Convert.ToInt32(Request.QueryString["technicianID"]);

                //Get the variables from the form
                string firstName = txtTechnicianFirst.Text;
                string lastName = txtTechnicianLast.Text;
                string email = txtTechnicianEmail.Text;
                string password = txtTechnicianPassword.Text;

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "UPDATE Technician SET FirstName=@firstName, LastName=@lastName, Pass=@password, Email=@email WHERE ID=@technicianID;";
                comm.Parameters.AddWithValue("@technicianID", technicianID);
                comm.Parameters.AddWithValue("@firstName", firstName);
                comm.Parameters.AddWithValue("@lastName", lastName);
                comm.Parameters.AddWithValue("@password", password);
                comm.Parameters.AddWithValue("@email", email);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Technician Updated.");
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
            string messageBoxText = "Are you sure you want to Delete this Technician?";
            string caption = "Delete Technician Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the inspectionID
                int technicianID = Convert.ToInt32(Request.QueryString["technicianID"]);

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "DELETE FROM Technician WHERE ID = @technicianID";
                comm.Parameters.AddWithValue("@technicianID", technicianID);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Technician Deleted.");
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