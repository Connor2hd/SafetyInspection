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
    public partial class CorrectiveActions : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Globals.role == "Tech" | Globals.role == "Admin")
            //{
            //    if (!IsPostBack)
            //    {
            //        ddlSchool.Items.Add("All");
            //        ddlSchool.Items.Add("Media");
            //        ddlSchool.Items.Add("Trades");
            //        ddlSchool.Items.Add("Technology");

            //        ddlStatus.Items.Add("All");
            //        ddlStatus.Items.Add("Upcoming");
            //        ddlStatus.Items.Add("Overdue");
            //        ddlStatus.Items.Add("Complete");

            //        labDropdown();
            //        technicianDropdown();
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
                ddlSchool.Items.Add("All");
                ddlSchool.Items.Add("Media");
                ddlSchool.Items.Add("Trades");
                ddlSchool.Items.Add("Technology");

                ddlStatus.Items.Add("All");
                ddlStatus.Items.Add("Upcoming");
                ddlStatus.Items.Add("Overdue");
                ddlStatus.Items.Add("Complete");

                labDropdown();
                technicianDropdown();
            }
            lblName1.Text = Globals.firstName + " " + Globals.lastName;
            lblName2.Text = Globals.firstName + " " + Globals.lastName;
        }

        protected void labDropdown()
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
                ddlLab.Items.Add(labName + " - " + labID);
            }

            //Close the connection
            conn.Close();
        }

        protected void technicianDropdown()
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Variable to hold the HTML text in
            string html = "";

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Get filters
            string parameters = GetParameters();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM CorrectiveActions INNER JOIN Lab ON CorrectiveActions.LabID = Lab.ID WHERE " + parameters;

            //Logic to determine if parameters is blank
            if (parameters.Length == 0)
            {
                comm.CommandText = "SELECT * FROM CorrectiveActions";
            }

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                //Hold reader values in variables for easier access
                int ID = reader.GetInt32(0);
                int inspectionID = reader.GetInt32(1);
                string labName = DataLookup.GetLabName(reader.GetInt32(2));
                string technicianName = DataLookup.GetTechName(reader.GetInt32(3));
                string hazardName = DataLookup.GetHazardName(reader.GetInt32(4));
                string areaName = DataLookup.GetAreaName(reader.GetInt32(5));
                string detailDescription = reader.GetString(6);
                string actionDescription = reader.GetString(7);
                DateTime dueDate = reader.GetDateTime(8);
                bool isComplete = reader.GetBoolean(9);

                string status = "";

                if (isComplete == true)
                {
                    status = "Complete";
                }

                if (isComplete == false && dueDate > DateTime.Now)
                {
                    status = "Pending";
                }

                if (isComplete == false && dueDate < DateTime.Now)
                {
                    status = "Overdue";
                }

                html += "<tr><td>" + ID + "</td><td>" + status + "</td><td>" + inspectionID + "</td><td>" + labName + "</td><td>" + technicianName + "</td><td>" + hazardName + "</td><td>" + areaName + "</td><td>" + detailDescription + "</td><td>" + actionDescription + "</td><td>" + dueDate + "</td><td><a href='ActionView.aspx?ID=" + ID + "'><button>View</button></a></td></tr>";
                results.InnerHtml = html;
            }

            //Close the connection
            conn.Close();
        }

        private string GetParameters()
        {
            //Blank string
            string parameters = "";

            if (txtCorrectiveActionID.Text.Length > 0)
            {
                parameters += "CorrectiveActions.ID = " + Convert.ToInt32(txtCorrectiveActionID.Text);
            }

            //Get the inspection ID if entered
            if (txtInspectionID.Text.Length > 0)
            {
                if (txtCorrectiveActionID.Text.Length > 0)
                {
                    parameters += "AND inspectionID = " + Convert.ToInt32(txtInspectionID.Text);
                }
                else
                {
                    parameters += "inspectionID = " + Convert.ToInt32(txtInspectionID.Text);
                }
            }

            //Get the labID if it is something other than all
            if (ddlLab.SelectedItem.Text != "All")
            {
                string room = ddlLab.SelectedItem.Text;
                var splitValueLab = room.Split(' ');
                int labID = Convert.ToInt16(splitValueLab[2]);

                if (txtCorrectiveActionID.Text.Length > 0 | txtInspectionID.Text.Length > 0)
                {
                    parameters += "AND labID = " + labID;
                }
                else
                {
                    parameters += "labID = " + labID;
                }
            }

            //Get the technician ID if it is something other than all
            if (ddlTechnician.SelectedItem.Text != "All")
            {
                string technician = ddlTechnician.SelectedItem.Text;
                var splitValueTech = technician.Split(' ');
                int technicianID = Convert.ToInt16(splitValueTech[3]);

                if (txtCorrectiveActionID.Text.Length > 0 | txtInspectionID.Text.Length > 0 | ddlLab.SelectedItem.Text != "All")
                {
                    parameters += "AND technicianID = " + technicianID;
                }
                else
                {
                    parameters += "technicianID = " + technicianID;
                }
            }

            //Get the school if it is something other than all
            if (ddlSchool.SelectedItem.Text != "All")
            {
                string school = ddlSchool.SelectedItem.Text;

                if (txtCorrectiveActionID.Text.Length > 0 | txtInspectionID.Text.Length > 0 | ddlLab.SelectedItem.Text != "All" | ddlTechnician.SelectedItem.Text != "All")
                {
                    parameters += "AND School = " + ddlSchool.SelectedItem.Text;
                }
                else
                {
                    parameters += "School = '" + ddlSchool.SelectedItem.Text + "'";
                }
            }

            //Get the due date if the callendar is not the current time
            if (calDueDate != null)
            {
                int result = DateTime.Compare(calDueDate.SelectedDate, new DateTime(0001, 01, 01));

                if (result > 0 || result < 0)
                {
                    DateTime dueDate = calDueDate.SelectedDate;

                    if (txtCorrectiveActionID.Text.Length > 0 | txtInspectionID.Text.Length > 0 | ddlLab.SelectedItem.Text != "All" | ddlTechnician.SelectedItem.Text != "All" | ddlSchool.SelectedItem.Text != "All")
                    {
                        parameters += "AND DueDate = " + dueDate;
                    }
                    else
                    {
                        parameters += "DueDate = " + dueDate;
                    }
                }
            }

            //Return the paramaters
            return parameters;
        }
    }
}