using NiagaraSafeFinal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace SafetyAuth
{
    public partial class InspectionEdit : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Globals.role == "Tech" | Globals.role == "Admin")
            {
                if (!IsPostBack)
                {
                    loadData();
                    labDropdown();
                    technicianDropdown();
                }
                lblName1.Text = Globals.firstName + " " + Globals.lastName;
                lblName2.Text = Globals.firstName + " " + Globals.lastName;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void loadData()
        {
            //Get the inspectionID
            int inspectionID = Convert.ToInt32(Request.QueryString["inspectionID"]);

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Inspection WHERE ID = " + inspectionID;

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Assign values to variables for easier access
                int technicianID = reader.GetInt32(1);
                string technicianName = DataLookup.GetTechName(technicianID);
                int labID = reader.GetInt32(2);
                string labName = DataLookup.GetLabName(labID);

                DateTime assignedDate = reader.IsDBNull(3)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(3);

                DateTime canStartDate = reader.IsDBNull(4)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(4);

                DateTime dueDate = reader.IsDBNull(5)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(5);

                DateTime startDate = reader.IsDBNull(6)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(6);

                DateTime finishDate = reader.IsDBNull(7)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(7);

                bool isComplete = reader.GetBoolean(8);

                //Insert variable values into display labels
                lblBigInspectionID.Text = inspectionID.ToString();
                txtInspectionID.Text = inspectionID.ToString();

                //Fill the dropdown list
                if (isComplete == true)
                {
                    ddlInspectionStatus.Items.Add("Complete");
                    ddlInspectionStatus.Items.Add("Incomplete");
                }

                if (isComplete == false)
                {
                    ddlInspectionStatus.Items.Add("Incomplete");
                    ddlInspectionStatus.Items.Add("Complete");
                }

                //Fill the calendars
                calDateAssigned.SelectedDate = assignedDate;
                calDateCanStart.SelectedDate = canStartDate;
                calDueDate.SelectedDate = dueDate;
                calStartDate.SelectedDate = startDate;
                calFinishDate.SelectedDate = finishDate;

                //Fill the lab drop down with the current value first
                currentLabItem(labID);

                //Fill the technician drop down with the current value first
                currentTechnicianItem(technicianID);
            }

            //Close the connection
            conn.Close();
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
                ddlRoom.Items.Add(labID + " - " + labName);
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
                ddlRoom.Items.Add(labID + " - " + labName);
            }

            //Close the connection
            conn.Close();
        }

        protected void technicianDropdown()
        {
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

        protected void currentTechnicianItem(int technicianID)
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Technician WHERE ID = " + technicianID;

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            //Create a lab item for the dropdown using the reader
            while (reader.Read())
            {
                string firstName = reader.GetString(1);
                string lastName = reader.GetString(2);
                ddlTechnician.Items.Add(lastName + ", " + firstName + " - " + technicianID);
            }

            //Close the connection
            conn.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Are you sure you want to save this inspection?";
            string caption = "Inspection Edit Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //The initial inspectionID value
                int initialInspectionID = Convert.ToInt32(Request.QueryString["inspectionID"]);

                //Get values from textboxes and store them in variables for later execution in update query
                int inspectionID = Convert.ToInt32(txtInspectionID.Text);
                DateTime assignedDate = calDateAssigned.SelectedDate;
                DateTime canStartDate = calDateCanStart.SelectedDate;
                DateTime dueDate = calDueDate.SelectedDate;
                DateTime startDate = calStartDate.SelectedDate;
                DateTime finishDate = calFinishDate.SelectedDate;
                int isCompleteBit = 1;

                //Get the technician ID from the dropdown item
                string technician = ddlTechnician.SelectedItem.Text;
                var splitValueTech = technician.Split(' ');
                int technicianID = Convert.ToInt16(splitValueTech[3]);

                //Get the lab ID from the dropdown item
                string room = ddlRoom.SelectedItem.Text;
                var splitValueLab = room.Split(' ');
                int labID = Convert.ToInt16(splitValueLab[0]);

                if (ddlInspectionStatus.SelectedItem.Value == "Complete")
                {
                    isCompleteBit = 1;
                }
                if (ddlInspectionStatus.SelectedItem.Value == "Incomplete")
                {
                    isCompleteBit = 0;
                }

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "UPDATE Inspection SET TechnicianID=@technicianID, labID=@labID, AssignedDate=@assignedDate, CanStartDate=@canStartDate, DueDate=@dueDate, StartDate=@startDate, FinishDate=@finishDate, IsComplete=@isCompleteBit WHERE ID = @inspectionID";

                comm.Parameters.AddWithValue("@technicianID", technicianID);
                comm.Parameters.AddWithValue("@labID", labID);
                comm.Parameters.AddWithValue("@assignedDate", assignedDate);
                comm.Parameters.AddWithValue("@canStartDate", canStartDate);
                comm.Parameters.AddWithValue("@dueDate", dueDate);
                comm.Parameters.AddWithValue("@startDate", startDate);
                comm.Parameters.AddWithValue("@finishDate", finishDate);
                comm.Parameters.AddWithValue("@isCompleteBit", isCompleteBit);
                comm.Parameters.AddWithValue("@inspectionID", inspectionID);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Shows feedback based on the success of the transaction
                if (rows > 0)
                {
                    MessageBox.Show("Inspection #" + inspectionID + " has been updated.");
                }
                else
                {
                    MessageBox.Show("Error. Inspection #" + inspectionID + " has not been updated.");
                }
            }
            if (result == MessageBoxResult.No | result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("No Action Taken");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int inspectionID = Convert.ToInt32(Request.QueryString["inspectionID"]);
            Response.Redirect("InspectionView.aspx?inspectionID=" + inspectionID);
        }
    }
}