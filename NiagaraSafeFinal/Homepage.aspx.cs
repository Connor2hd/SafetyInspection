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
    public partial class Homepage : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Globals.role == "Tech" | Globals.role == "Admin")
            //{
            //    if (!IsPostBack)
            //    {
            //        loadCompleted();
            //        loadPending();
            //        loadOverdue();
            //        statBoxes();
            //    }
            //    lblName.Text = Globals.firstName + " " + Globals.lastName;
            //    lblName2.Text = Globals.firstName + " " + Globals.lastName;
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}
            if (!IsPostBack)
            {
                loadCompleted();
                loadPending();
                loadOverdue();
                statBoxes();
            }
            lblName.Text = Globals.firstName + " " + Globals.lastName;
            lblName2.Text = Globals.firstName + " " + Globals.lastName;
        }

        protected void statBoxes()
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command for each stat box
            SqlCommand comm1 = conn.CreateCommand();
            SqlCommand comm2 = conn.CreateCommand();
            SqlCommand comm3 = conn.CreateCommand();
            SqlCommand comm4 = conn.CreateCommand();
            SqlCommand comm5 = conn.CreateCommand();
            SqlCommand comm6 = conn.CreateCommand();

            //Completed on time
            comm1.CommandText = "SELECT COUNT(*) FROM Inspection WHERE IsComplete = 1 AND FinishDate <= DueDate";
            //Pending
            comm2.CommandText = "SELECT COUNT(*) FROM Inspection WHERE IsComplete = 0 AND DueDate > GETDATE()";
            //Overdue
            comm3.CommandText = "SELECT COUNT(*) FROM Inspection WHERE IsComplete = 0 AND DueDate < GETDATE()";
            //Corrective Actions
            comm4.CommandText = "SELECT COUNT(*) FROM CorrectiveActions WHERE IsComplete = 0";
            //Completed overdue
            comm5.CommandText = "SELECT COUNT(*) FROM Inspection WHERE IsComplete = 1 AND FinishDate >= DueDate";
            //Absolute Total
            comm6.CommandText = "SELECT COUNT(*) FROM Inspection";

            //Open the connection
            conn.Open();

            //Execute querys
            decimal completeOnTime = Convert.ToDecimal(comm1.ExecuteScalar());
            decimal completeOverdue = Convert.ToDecimal(comm5.ExecuteScalar());
            decimal pending = Convert.ToDecimal(comm2.ExecuteScalar());
            decimal overdue = Convert.ToDecimal(comm3.ExecuteScalar());
            decimal pendingAction = Convert.ToDecimal(comm4.ExecuteScalar());
            decimal total = Convert.ToDecimal(comm6.ExecuteScalar());

            //Close the connection
            conn.Close();

            //Calculate completion percentage for stat box 1
            decimal onTime = ((completeOnTime / total) * 100);   

            string percent = string.Format("{0:0.00}", onTime) + "%";

            //Assign data to stat boxes
            completedOnTimeStat.InnerHtml = percent;
            pendingReportsStat.InnerHtml = pending.ToString();
            overdueReportsStat.InnerHtml = overdue.ToString();
            correctiveActionsStat.InnerHtml = pendingAction.ToString();
        }

        protected void loadCompleted()
        {
            //Variable to hold the HTML text in
            string html = "";

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT TOP 4 * FROM Inspection WHERE IsComplete = 1 ORDER BY DueDate DESC";

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

                html += "<a href='../InspectionView.aspx?inspectionID=" + ID + "' class='list-group-item' id=''>Inspection ID: " + ID + " - Room: " + labName + " - Due Date: " + dueDate + " - Technician: " + techName + "</a>";
                completedResults.InnerHtml = html;
            }

            //Close the connection
            conn.Close();
        }

        protected void loadPending()
        {
            //Variable to hold the HTML text in
            string html = "";

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT TOP 4 * FROM Inspection WHERE IsComplete = 0 AND DueDate < GETDATE() ORDER BY DueDate DESC";

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

                html += "<a href='../InspectionView.aspx?inspectionID=" + ID + "' class='list-group-item' id=''>Inspection ID: " + ID + " - Room: " + labName + " - Due Date: " + dueDate + " - Technician: " + techName + "</a>";
                pendingResults.InnerHtml = html;
            }

            //Close the connection
            conn.Close();
        }

        protected void loadOverdue()
        {
            //Variable to hold the HTML text in
            string html = "";

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT TOP 4 * FROM Inspection WHERE IsComplete = 0 AND DueDate > GETDATE() ORDER BY DueDate DESC";

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

                html += "<a href='../InspectionView.aspx?inspectionID=" + ID + "' class='list-group-item' id=''>Inspection ID: " + ID + " - Room: " + labName + " - Due Date: " + dueDate + " - Technician: " + techName + "</a>";
                overdueResults.InnerHtml = html;
            }

            //Close the connection
            conn.Close();
        }
    }
}