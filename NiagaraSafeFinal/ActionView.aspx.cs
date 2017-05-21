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
    public partial class ActionView : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Globals.role == "Tech" | Globals.role == "Admin")
            //{
            //    if (!IsPostBack)
            //    {
            //        loadData();
            //        loadAction();
            //    }
            //    lblName1.Text = Globals.firstName + " " + Globals.lastName;
            //    lblName2.Text = Globals.firstName + " " + Globals.lastName;
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}
            if (!IsPostBack)
            {
                loadData();
                loadAction();
            }
            lblName1.Text = Globals.firstName + " " + Globals.lastName;
            lblName2.Text = Globals.firstName + " " + Globals.lastName;
        }

        protected void loadData()
        {
            //Get the Action ID
            int ID = Convert.ToInt32(Request.QueryString["ID"]);

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT CorrectiveActions.ID, CorrectiveActions.InspectionID, CorrectiveActions.TechnicianID, CorrectiveActions.LabID, AssignedDate, CanStartDate, CorrectiveActions.DueDate, StartDate, FinishDate, CorrectiveActions.IsComplete FROM CorrectiveActions INNER JOIN Inspection ON CorrectiveActions.InspectionID = Inspection.ID WHERE CorrectiveActions.ID = " + ID;

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Assign values to variables for easier access
                int inspectionID = reader.GetInt32(1);
                int technicianID = reader.GetInt32(2);
                string technicianName = DataLookup.GetTechName(technicianID);
                int labID = reader.GetInt32(3);
                string labName = DataLookup.GetLabName(labID);

                DateTime assignedDate = reader.IsDBNull(4)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(4);

                DateTime canStartDate = reader.IsDBNull(5)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(5);

                DateTime dueDate = reader.IsDBNull(6)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(6);

                DateTime startDate = reader.IsDBNull(7)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(7);

                DateTime finishDate = reader.IsDBNull(8)
                    ? DateTime.MaxValue
                    : reader.GetDateTime(8);

                bool isComplete = reader.GetBoolean(9);

                //Determine inspection status
                string status = "";
                if (isComplete == true)
                {
                    status = "Complete";
                }
                if (isComplete == false && (dueDate < DateTime.Now))
                {
                    status = "Overdue";
                }
                if (isComplete == false && (dueDate > DateTime.Now))
                {
                    status = "Pending";
                }

                //Insert variable values into display labels
                lblBigInspectionID.Text = ID.ToString();
                lblInspectionID.Text = inspectionID.ToString();
                lblTechnicianID.Text = technicianID.ToString();
                lblRoomID.Text = labID.ToString();
                lblInspectionStatus.Text = status;
                lblTechnicianName.Text = technicianName;
                lblRoomName.Text = labName;

                lblAssigned.Text = assignedDate.ToString("MMMM dd, yyyy");
                lblCanStart.Text = canStartDate.ToString("MMMM dd, yyyy");
                lblDueDate.Text = dueDate.ToString("MMMM dd, yyyy");
                lblStartDate.Text = startDate.ToString("MMMM dd, yyyy");
                lblFinishDate.Text = finishDate.ToString("MMMM dd, yyyy");

                if (finishDate == DateTime.MaxValue)
                {
                    lblFinishDate.Text = "";
                }

                if (startDate == DateTime.MaxValue)
                {
                    lblStartDate.Text = "";
                }

                if (assignedDate == DateTime.MaxValue)
                {
                    lblAssigned.Text = "";
                }

                if (dueDate == DateTime.MaxValue)
                {
                    lblDueDate.Text = "";
                }

                if (canStartDate == DateTime.MaxValue)
                {
                    lblCanStart.Text = "";
                }
            }

            //Close the connection
            conn.Close();
        }

        protected void loadAction()
        {
            //Variable to hold the HTML text in
            string html = "";

            //Get the Action ID
            int ID = Convert.ToInt32(Request.QueryString["ID"]);

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM CorrectiveActions WHERE ID = " + ID;

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Assign values to variables for easier access;
                int labID = reader.GetInt32(2);
                int technicianID = reader.GetInt32(3);
                int hazardID = reader.GetInt32(4);
                string hazardName = DataLookup.GetHazardName(hazardID);
                int areaID = reader.GetInt32(5);
                string areaName = DataLookup.GetAreaName(areaID);
                string detailDescription = reader.GetString(6);
                string actionDescription = reader.GetString(7);
                DateTime dueDate = reader.GetDateTime(8);
                bool isComplete = reader.GetBoolean(9);

                //Determine corrective action status
                string status = "";
                if (isComplete == true)
                {
                    status = "Complete";
                }
                if (isComplete == false && (dueDate < DateTime.Now))
                {
                    status = "Overdue";
                }
                if (isComplete == false && (dueDate > DateTime.Now))
                {
                    status = "Pending";
                }

                //Create a table using a string value and assign it to the elements inner html
                html += "<tr><td>" + ID + "</td><td>" + hazardName + "</td><td>" + areaName + "</td><td>" + detailDescription + "</td><td>" + actionDescription + "</td><td>" + dueDate + "</td><td>" + status + "</td>";
                results.InnerHtml = html;
            }

            //Close the connection
            conn.Close();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Create a warning button so the user know what they are doing
            string messageBoxText = "Are you sure you want to delete this corrective action?";
            string caption = "Delete Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Variable to retrieve the ID of the record that will be deleted
                int ID = Convert.ToInt32(Request.QueryString["ID"]);

                //Assign the connection string to a sqlconnection object
                SqlConnection conn = new SqlConnection(connectionString);

                //Store the command to be executed in a variable
                string query = "DELETE FROM CorrectiveActions WHERE ID = " + ID;

                //Create a command object with the query string
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the commands text
                comm.CommandText = query;

                //Open the server connection
                conn.Open();

                //Execute query
                comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Return the labID
                MessageBox.Show("Record Deleted");

                //Redirect to homepage
                Response.Redirect("Homepage.aspx");
            }
            if (result == MessageBoxResult.No | result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("Record Not Deleted.");
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //Get the ID
            int ID = Convert.ToInt32(Request.QueryString["ID"]);

            //Redirect to the edit page with the inspection ID
            Response.Redirect("ActionEdit.aspx?ID=" + ID);
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Are you sure you want to mark this corrective action as complete?";
            string caption = "Corrective Action Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Variable to retrieve the ID of the record that will be deleted
                int ID = Convert.ToInt32(Request.QueryString["ID"]);

                //Assign the connection string to a sqlconnection object
                SqlConnection conn = new SqlConnection(connectionString);

                //Store the command to be executed in a variable
                string query = "UPDATE CorrectiveActions SET IsComplete = 1 WHERE ID = " + ID;

                //Create a command object with the query string
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the commands text
                comm.CommandText = query;

                //Open the server connection
                conn.Open();

                //Execute query
                int affectedRows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Messagebox to show if record was updated successfuly
                if (affectedRows > 0)
                {
                    MessageBox.Show("Corrective action has been marked as complete");
                }
                else
                {
                    MessageBox.Show("Corrective action has not been changed");
                }
            }
            if (result == MessageBoxResult.No | result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("No Action Taken");
            }
        }
    }
}