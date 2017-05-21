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
    public partial class DatabaseAdminArea : System.Web.UI.Page
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
            //Get the ID
            int areaID = Convert.ToInt32(Request.QueryString["areaID"]);

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Area WHERE ID=@areaID";
            comm.Parameters.AddWithValue("@areaID", areaID);

            //Open the connection
            conn.Open();

            //Execute the command
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Fill the textboxes
                txtAreaID.Text = reader.GetInt32(0).ToString();
                txtAreaName.Text = reader.GetString(2);
                int labID = reader.GetInt32(1);
                string labName = DataLookup.GetLabName(labID);

                ddlLab.Items.Add(labID + " - " + labName);
            }

            //Close the connection
            conn.Close();

            lblName1.Text = Globals.firstName + " " + Globals.lastName;
            lblName2.Text = Globals.firstName + " " + Globals.lastName;

            if (!IsPostBack)
            {
                //Populate labdropdown
                labDropdown();
            }
        }

        protected void labDropdown()
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Lab";

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                int labID = reader.GetInt32(0);
                string labName = reader.GetString(1);

                //Add items to the technician and lab drop downs
                ddlLab.Items.Add(labID + " - " + labName);
            }

            //Close the connection
            conn.Close();
        }

        protected void currentLabItem(int labID)
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Lab WHERE ID = " + labID;

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            //Create a lab item for the dropdown using the reader
            while (reader.Read())
            {
                string labName = reader.GetString(1);
                ddlLab.Items.Add(labID + " - " + labName);
            }

            //Close the connection
            conn.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Are you sure you want to save this area?";
            string caption = "Hazard Edit Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the variables from the form
                int areaID = Convert.ToInt32(txtAreaID.Text);
                string areaName = txtAreaName.Text;

                //Get the lab ID from the dropdown item
                string lab = ddlLab.SelectedItem.Text.ToString();
                var splitValueLab = lab.Split(' ');
                int labID = Convert.ToInt16(splitValueLab[0]);

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "UPDATE Area SET LabID=@labID, Title=@areaName WHERE ID= " + areaID;
                comm.Parameters.AddWithValue("@areaName", areaName);
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
                    MessageBox.Show("Area Updated.");
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
            string messageBoxText = "Are you sure you want to delete this area?";
            string caption = "Delete Hazard Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the hazard ID from the textbox
                int areaID = Convert.ToInt32(txtAreaID.Text);

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "DELETE FROM Area WHERE ID = @areaID";
                comm.Parameters.AddWithValue("@areaID", areaID);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Area Deleted.");
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