using NiagaraSafeFinal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace SafetyAuth
{
    public partial class ActionEdit : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Globals.role == "Tech" | Globals.role == "Admin") {
            //    if (!IsPostBack)
            //    {
            //        loadData();
            //        labDropdown();
            //        technicianDropdown();
            //        areaDropdown();
            //        hazardDropdown();
            //    }
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}
            if (!IsPostBack)
            {
                loadData();
                labDropdown();
                technicianDropdown();
                areaDropdown();
                hazardDropdown();
            }
        }

        protected void loadData()
        {
            //Get the actionID
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
                //Assign values to variables for easier access
                int inspectionID = reader.GetInt32(1);
                int labID = reader.GetInt32(2);
                int technicianID = reader.GetInt32(3);
                int hazardID = reader.GetInt32(4);
                int areaID = reader.GetInt32(5);
                string hazardOther = reader.GetString(10);
                string areaOther = reader.GetString(11);
                string detailDesc = reader.GetString(6);
                string actionDesc = reader.GetString(7);
                DateTime dueDate = reader.GetDateTime(8);
                bool isComplete = reader.GetBoolean(9);

                //Insert variable values into display labels
                lblBigID.Text = ID.ToString();
                txtInspectionID.Text = inspectionID.ToString();

                //Fill the textboxes
                lblBigID.Text = ID.ToString();
                txtInspectionID.Text = inspectionID.ToString();
                txtArea.Text = areaOther;
                txtHazard.Text = hazardOther;
                txtDetailDesc.Text = detailDesc;
                txtActionDesc.Text = actionDesc;

                //Fill the lab dropdowns with their current values first
                currentLabItem(labID);
                currentTechnicianItem(technicianID);
                currentArea(areaID);
                currentHazard(hazardID);

                //Set the callendar
                calDueDate.SelectedDate = dueDate;

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
                ddlTechnician.Items.Add(technicianID + " - " + firstName + ", " + lastName);
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
                ddlTechnician.Items.Add(technicianID + " - " + firstName + ", " + lastName);
            }

            //Close the connection
            conn.Close();
        }

        protected void areaDropdown()
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT Area.ID, Area.Title, Lab.Title FROM Area INNER JOIN Lab ON Area.LabID = Lab.ID";

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                int areaID = reader.GetInt32(0);
                string areaName = reader.GetString(1);
                string labName = reader.GetString(2);

                //Add items to the technician and lab drop downs
                ddlArea.Items.Add(areaID + " - Lab: " + labName + " - Area: " + areaName);
            }

            //Close the connection
            conn.Close();
        }

        protected void currentArea(int areaID)
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT Area.ID, Area.Title, Lab.Title FROM Area INNER JOIN Lab ON Area.LabID = Lab.ID WHERE area.ID = " + areaID;

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            //Create a lab item for the dropdown using the reader
            while (reader.Read())
            {
                areaID = reader.GetInt32(0);
                string areaName = reader.GetString(1);
                string labName = reader.GetString(2);

                //Add items to the technician and lab drop downs
                ddlArea.Items.Add(areaID + " - Lab: " + labName + " - Area: " + areaName);
            }

            //Close the connection
            conn.Close();
        }

        protected void hazardDropdown()
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
                string hazardDesc = reader.GetString(1);

                //Add the items to the hazard dropdown
                ddlHazard.Items.Add(hazardID + " - Description: " + hazardDesc);
            }

            //Close the connection
            conn.Close();
        }

        protected void currentHazard(int hazardID)
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Hazard WHERE ID = " + hazardID;

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                hazardID = reader.GetInt32(0);
                string hazardDesc = reader.GetString(1);

                //Add the items to the hazard dropdown
                ddlHazard.Items.Add(hazardID + " - Description: " + hazardDesc);
            }

            //Close the connection
            conn.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Are you sure you want to save this corrective action?";
            string caption = "Corrective Action Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the actionID
                int actionID = Convert.ToInt32(Request.QueryString["ID"]);

                //Get values from textboxes and store them in variables for later execution in update query
                int inspectionID = Convert.ToInt32(txtInspectionID.Text);
                string areaOther = txtArea.Text;
                string hazardOther = txtHazard.Text;
                string detailDesc = txtDetailDesc.Text;
                string actionDesc = txtActionDesc.Text;
                int isCompleteBit = 0;

                //Callendar Value
                DateTime dueDate = calDueDate.SelectedDate;

                //Get the technician ID from the dropdown item
                string technician = ddlTechnician.SelectedItem.Text;
                var splitValueTech = technician.Split(' ');
                int technicianID = Convert.ToInt16(splitValueTech[0]);

                //Get the lab ID from the dropdown item
                string room = ddlRoom.SelectedItem.Text;
                var splitValueLab = room.Split(' ');
                int labID = Convert.ToInt32(splitValueLab[0]);

                //Get the areaID from the dropdown item
                string area = ddlArea.SelectedItem.Text;
                var splitValueArea = area.Split(' ');
                int areaID = Convert.ToInt32(splitValueArea[0]);

                //Get the Hazard ID or Hazard other
                string hazard = ddlHazard.SelectedItem.Text;
                var splitValueHazard = hazard.Split(' ');
                int hazardID = Convert.ToInt32(splitValueHazard[0]);

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
                comm.CommandText = "UPDATE CorrectiveActions SET InspectionID = @inspectionID, TechnicianID = @technicianID, LabID = @labID, IsComplete = @isComplete, AreaID = @areaID, AreaOther = @areaOther, HazardID = @hazardID, hazardOther = @hazardOther, DetailDescription = @detailDesc, ActionDescription = @actionDesc, DueDate = @dueDate WHERE ID = @ID";

                comm.Parameters.AddWithValue("@inspectionID", inspectionID);
                comm.Parameters.AddWithValue("@ID", actionID);
                comm.Parameters.AddWithValue("@technicianID", technicianID);
                comm.Parameters.AddWithValue("@labID", labID);
                comm.Parameters.AddWithValue("@isComplete", isCompleteBit);
                comm.Parameters.AddWithValue("@areaID", areaID);
                comm.Parameters.AddWithValue("@areaOther", areaOther);
                comm.Parameters.AddWithValue("@hazardID", hazardID);
                comm.Parameters.AddWithValue("@hazardOther", hazardOther);
                comm.Parameters.AddWithValue("@detailDesc", detailDesc);
                comm.Parameters.AddWithValue("@actionDesc", actionDesc);
                comm.Parameters.Add("@dueDate", SqlDbType.DateTime).Value = dueDate;

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Shows feedback based on the success of the transaction
                if (rows > 0)
                {
                    MessageBox.Show("Corrective Action #" + actionID + " has been updated.");
                }
                else
                {
                    MessageBox.Show("Error. Corretive Action #" + actionID + " has not been updated.");
                }

                Response.Redirect("ActionView.aspx?ID=" + actionID);

            }
            if (result == MessageBoxResult.No | result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("No Action Taken");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(lblBigID.Text);
            Response.Redirect("ActionView.aspx?ID=" + ID);
        }
    }
}