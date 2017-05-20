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
    public partial class DatabaseAdminHazard : System.Web.UI.Page
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
            int hazardID = Convert.ToInt32(Request.QueryString["hazardID"]);

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Hazard WHERE ID=@hazardID";
            comm.Parameters.AddWithValue("@hazardID", hazardID);

            //Open the connection
            conn.Open();

            //Execute the command
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Fill the textboxes
                txtHazardID.Text = reader.GetInt32(0).ToString();
                txtHazardName.Text = reader.GetString(1);
            }

            //Close the connection
            conn.Close();

            lblName1.Text = Globals.firstName + " " + Globals.lastName;
            lblName2.Text = Globals.firstName + " " + Globals.lastName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Are you sure you want to save this hazard?";
            string caption = "Hazard Edit Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the variables from the form
                int hazardID = Convert.ToInt32(txtHazardID.Text);
                string hazardName = txtHazardName.Text;

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "UPDATE Hazard SET HazardDescription=@hazardName WHERE ID= " + hazardID;
                comm.Parameters.AddWithValue("@hazardName", hazardName);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Hazard Updated.");
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
            string messageBoxText = "Are you sure you want to delete this hazard?";
            string caption = "Delete Hazard Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the hazard ID from the textbox
                int hazardID = Convert.ToInt32(txtHazardID.Text);

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "DELETE FROM Hazard WHERE ID = @hazardID";
                comm.Parameters.AddWithValue("@hazardID", hazardID);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Hazard Deleted.");
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