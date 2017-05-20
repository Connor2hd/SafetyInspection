using NiagaraSafeFinal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SafetyAuth
{
    public partial class InspectionAssigned : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Globals.role == "Tech" | Globals.role == "Admin")
            {
                if (!IsPostBack)
                {
                    LoadPending();
                    LoadStarted();
                    LoadCompleted();
                }
                lblName1.Text = Globals.firstName + " " + Globals.lastName;
                lblName2.Text = Globals.firstName + " " + Globals.lastName;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void LoadPending()
        {
            //Variable to hold the HTML text in
            string html = "";

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Inspection WHERE IsStarted = 0 AND TechnicianID=@technicianID";
            comm.Parameters.AddWithValue("technicianID", Globals.technicianID);

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Get values from the reader and store them in variables for easier access and reading
                int ID = reader.GetInt32(0);
                int technicianID = reader.GetInt32(1);
                string techName = DataLookup.GetTechName(technicianID);
                int labID = reader.GetInt32(2);
                string labName = DataLookup.GetLabName(labID);
                DateTime dueDate = reader.GetDateTime(5);

                //This commented code here is if the desired outcome was the 'list group items' that I made.  I thought they might look better than tables but
                //in this situation I am not too sure. There is code for both.  Commenting and uncommenting will switch.

                html += "<a href='../InspectionCreateStatic.aspx?ID=" + ID + "' class='list-group-item' id=''>Inspection ID: " + ID + " - Room: " + labName + " - Due Date: " + dueDate + " - Technician: " + techName + "</a>";

                //html += "<tr><td>" + ID + "</td><td>" + techName + "</td><td>" + labName + "</td><td>" + dueDate + "</td><td><a href='InspectionCreateStatic.aspx?ID=" + ID + "'><button>Start</button></a></td></tr>";
                pendingResults.InnerHtml = html;
            }

            //Close the connection
            conn.Close();
        }

        protected void LoadStarted()
        {
            //Variable to hold the HTML text in
            string html = "";

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Inspection WHERE IsStarted = 1 AND TechnicianID=@technicianID";
            comm.Parameters.AddWithValue("technicianID", Globals.technicianID);

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Get values from the reader and store them in variables for easier access and reading
                int ID = reader.GetInt32(0);
                int technicianID = reader.GetInt32(1);
                string techName = DataLookup.GetTechName(technicianID);
                int labID = reader.GetInt32(2);
                string labName = DataLookup.GetLabName(labID);
                DateTime dueDate = reader.GetDateTime(5);

                //This commented code here is if the desired outcome was the 'list group items' that I made.  I thought they might look better than tables but
                //in this situation I am not too sure. There is code for both.  Commenting and uncommenting will switch.

                html += "<a href='../InspectionCreateStatic.aspx?ID=" + ID + "' class='list-group-item' id=''>Inspection ID: " + ID + " - Room: " + labName + " - Due Date: " + dueDate + " - Technician: " + techName + "</a>";

                //html += "<tr><td>" + ID + "</td><td>" + techName + "</td><td>" + labName + "</td><td>" + dueDate + "</td><td><a href='InspectionEdit.aspx?ID=" + ID + "'><button>Continue</button></a></td></tr>";
                //html += "<tr><td>" + ID + "</td><td>" + techName + "</td><td>" + labName + "</td><td>" + dueDate + "</td><td><form action='InspectionEdit.aspx?ID=" + ID + "'><input type='submit' value='Start'></form></td></tr>";
                startedResults.InnerHtml = html;
            }

            //Close the connection
            conn.Close();
        }

        protected void LoadCompleted()
        {
            //Variable to hold the HTML text in
            string html = "";

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Inspection WHERE IsComplete = 1 AND TechnicianID=@technicianID";
            comm.Parameters.AddWithValue("technicianID", Globals.technicianID);

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Get values from the reader and store them in variables for easier access and reading
                int ID = reader.GetInt32(0);
                int technicianID = reader.GetInt32(1);
                string techName = DataLookup.GetTechName(technicianID);
                int labID = reader.GetInt32(2);
                string labName = DataLookup.GetLabName(labID);
                DateTime dueDate = reader.GetDateTime(5);
                DateTime startDate = reader.GetDateTime(6);
                DateTime finishDate = reader.GetDateTime(7);

                //This commented code here is if the desired outcome was the 'list group items' that I made.  I thought they might look better than tables but
                //in this situation I am not too sure. There is code for both.  Commenting and uncommenting will switch.

                html += "<a href='../InspectionCreate.aspx?inspectionID=" + ID + "' class='list-group-item' id=''>Inspection ID: " + ID + " - Room: " + labName + " - Due Date: " + dueDate + " - Technician: " + techName + "</a>";

                //html += "<tr><td>" + ID + "</td><td>" + techName + "</td><td>" + labName + "</td><td>" + dueDate + "</td><td>" + startDate + "</td><td>" + finishDate + "</td><td><a href='InspectionView.aspx?inspectionID=" + ID + "'><button>View</button></a></td></tr>";
                completeResults.InnerHtml = html;
            }

            //Close the connection
            conn.Close();
        }
    }
}