using NiagaraSafeFinal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace SafetyAuth
{
    public partial class InspectionSchedule : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Globals.role == "Admin")
            {
                if (!IsPostBack)
                {
                    LoadLabs();
                    LoadTechnicians();
                }
                lblName1.Text = Globals.firstName + " " + Globals.lastName;
                lblName2.Text = Globals.firstName + " " + Globals.lastName;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void LoadLabs()
        {
            //Add an item for all labs
            ddlLab.Items.Add("All");

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

        protected void LoadTechnicians()
        {
            //Add an item for all labs
            ddlTechnician.Items.Add("All");

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Technician";

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                int technicianID = reader.GetInt32(0);
                string firstName = reader.GetString(1);
                string lastName = reader.GetString(2);

                //Add items to the technician and lab drop downs
                ddlTechnician.Items.Add(lastName + ", " + firstName + " - " + technicianID);
            }

            //Close the connection
            conn.Close();
        }

        protected void btnSchedule_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Are you sure you want to schedule this inspection?";
            string caption = "Inspection Schedule Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the lab ID
                string room = ddlLab.SelectedItem.Text;
                var splitValueLab = room.Split(' ');
                int labID = Convert.ToInt16(splitValueLab[0]);

                //Get the technician ID
                string technician = ddlTechnician.SelectedItem.Text;
                var splitValueTech = technician.Split(' ');
                int technicianID = Convert.ToInt16(splitValueTech[3]);

                //Get the due date value
                DateTime dueDate = calDueDate.SelectedDate;

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "INSERT INTO Inspection (TechnicianID, LabID, AssignedDate, DueDate) VALUES (@technicianID, @labID, @assignedDate, @dueDate)";
                comm.Parameters.Add("@technicianID", technicianID);
                comm.Parameters.Add("@labID", labID);
                comm.Parameters.Add("@assignedDate", SqlDbType.DateTime).Value = DateTime.Now;
                comm.Parameters.Add("@dueDate", SqlDbType.DateTime).Value = dueDate;

                //Open the connection
                conn.Open();

                //Execute the query
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Shows feedback based on the success of the transaction
                if (rows > 0)
                {
                    MessageBox.Show("Inspection has been scheduled.");
                    //Send a notification Email
                    string mailBody = "Hello " + technician + ", You have a new inspection in room " + room + " that is due on " + dueDate;
                    DataLookup.SendMail(technicianID, "You Have a New Scheduled Inspection", mailBody);
                }
                else
                {
                    MessageBox.Show("Error. Inspection  has not been scheduled.");
                }
            }
            if (result == MessageBoxResult.No | result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("No Action Taken");
            }
        }
    }
}