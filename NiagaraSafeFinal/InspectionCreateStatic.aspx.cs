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
    public partial class InspectionCreateStatic : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Globals.role == "Tech" | Globals.role == "Admin")
            //{
            //    loadData();

            //    if (!IsPostBack)
            //    {
            //        AreaDropdwon();
            //        HazardDropdown();
            //        LoadActions();
            //    }
            //    lblName1.Text = Globals.firstName + " " + Globals.lastName;
            //    lblName2.Text = Globals.firstName + " " + Globals.lastName;
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}
            loadData();

            if (!IsPostBack)
            {
                AreaDropdwon();
                HazardDropdown();
                LoadActions();
            }
            lblName1.Text = Globals.firstName + " " + Globals.lastName;
            lblName2.Text = Globals.firstName + " " + Globals.lastName;
        }

        protected void loadData()
        {
            //Get the inspectionID
            int inspectionID = Convert.ToInt32(Request.QueryString["ID"]);

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

                bool isComplete = reader.GetBoolean(8);

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
                lblInspectionID.Text = inspectionID.ToString();
                lblTechnicianID.Text = technicianID.ToString();
                lblRoomID.Text = labID.ToString();
                lblInspectionStatus.Text = status;
                lblTechnicianName.Text = technicianName;
                lblRoomName.Text = labName;

                lblAssigned.Text = assignedDate.ToString("MMMM dd, yyyy");
                lblCanStart.Text = canStartDate.ToString("MMMM dd, yyyy");
                lblDueDate.Text = dueDate.ToString("MMMM dd, yyyy");

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

        protected void LoadActions()
        {
            //Variable to hold the HTML text in
            string html = "";

            //Get the inspectionID
            int inspectionID = Convert.ToInt32(Request.QueryString["ID"]);

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM CorrectiveActions WHERE inspectionID = " + inspectionID;

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Assign values to variables for easier access
                int ID = reader.GetInt32(0);
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
        }

        protected void HazardDropdown()
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Hazard";

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                int hazardID = reader.GetInt32(0);
                string hazardName = reader.GetString(1);

                //Add items to the technician and lab drop downs
                ddlHazard.Items.Add(hazardID + "-" + hazardName);
            }

            //Close the connection
            conn.Close();
        }

        protected void AreaDropdwon()
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Area";

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                int areaID = reader.GetInt32(0);
                int labID = reader.GetInt32(1);
                string labName = DataLookup.GetLabName(labID);
                string areaName = reader.GetString(2);

                //Add items to the technician and lab drop downs
                ddlArea.Items.Add(areaID + "-" + labName + "-" + areaName);
            }

            //Close the connection
            conn.Close();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Inspection is technically already created.  The only data that needs to be added
            //iS the finish date and the hazard items

            string messageBoxText = "Are you sure you want to insert this corrective action?";
            string caption = "Corrective Action Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the inspectionID
                int inspectionID = Convert.ToInt32(Request.QueryString["ID"]);

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "UPDATE Inspection SET StartDate=@startDate, isStarted=1 WHERE ID = " + inspectionID;

                //Assign the current date time to the query
                comm.Parameters.AddWithValue("@startDate", DateTime.Now);

                //Open the connection
                conn.Open();

                //Execute the command
                comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Gather data to create insert query for hazard item
                int labID = Convert.ToInt32(lblRoomID.Text);
                int technicianID = Convert.ToInt32(lblTechnicianID.Text);

                string hazard = ddlHazard.SelectedItem.Text;
                var splitValueHazard = hazard.Split('-');
                int hazardID = Convert.ToInt16(splitValueHazard[0]);

                string area = ddlArea.SelectedItem.Text;
                var splitValueArea = area.Split('-');
                int areaID = Convert.ToInt16(splitValueArea[0]);

                string hazardOther = txtHazardOther.Text;
                string areaOther = txtArea.Text;
                string hazardDesc = txtHazardDesc.Text;
                string actionDesc = txtActionDesc.Text;
                DateTime dueDate = calDueDate.SelectedDate;

                //Execute the public method for inserting a hazard to an inspection
                int rows = DataLookup.InsertAction(inspectionID, labID, technicianID, hazardID, areaID, hazardDesc, actionDesc, dueDate, hazardOther, areaOther);

                //Shows feedback based on the success of the transaction
                if (rows > 0)
                {
                    MessageBox.Show("Hazard has been added to Inspection #" + inspectionID);
                }
                else
                {
                    MessageBox.Show("Error. No changes have been made.");
                }

                //Reload the page so another corrective action can be added
                Response.Redirect("InspectionCreateStatic.aspx?ID=" + inspectionID);
            }
            if (result == MessageBoxResult.No | result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("No Action Taken");
            }
        }

        protected void btnFinalize_Click(object sender, EventArgs e)
        {
            //Inspection is technically already created.  The only data that needs to be added
            //iS the finish date and the hazard items

            string messageBoxText = "Are you sure you want to finalize this inspection?";
            string caption = "Finalize Inspection Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the inspectionID
                int inspectionID = Convert.ToInt32(Request.QueryString["ID"]);

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "UPDATE Inspection SET FinishDate=@finishDate, IsComplete=1 WHERE ID = " + inspectionID;

                //Assign the current date time to the query
                comm.Parameters.AddWithValue("@finishDate", DateTime.Now);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Inspection Finalized");
                    //Reload the page
                    Response.Redirect("Homepage.aspx");
                    //Send out an email to the admin users
                    string mailBody = "Hello Administrator users.  Technician " + lblTechnicianName.Text + " has completed inspection " + inspectionID + " In the lab/room " + lblRoomName.Text + " At the time of " + DateTime.Now + ".";
                    DataLookup.SendMail(999, "An Inspection Has Been Completed", mailBody);
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
    }
}