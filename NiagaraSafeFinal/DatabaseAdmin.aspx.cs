using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using NiagaraSafeFinal;

namespace SafetyAuth
{
    public partial class DatabaseAdmin : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Globals.role == "Admin")
            {
                if (!IsPostBack)
                {
                    ddlSchool.Items.Add("Media");
                    ddlSchool.Items.Add("Trades");
                    ddlSchool.Items.Add("Technology");

                    LabDropdown();
                    TechnicianDropdown();
                    AreaDropdwon();
                    HazardDropdown();
                }
                lblName1.Text = Globals.firstName + " " + Globals.lastName;
                lblName2.Text = Globals.firstName + " " + Globals.lastName;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void LabDropdown()
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

                //Add items to the lab drop downs
                ddlLab1.Items.Add(labID + " " + labName);
                ddlLab2.Items.Add(labID + " " + labName);
            }

            //Close the connection
            conn.Close();
        }

        protected void TechnicianDropdown()
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

        protected void btnAddTech_Click(object sender, EventArgs e)
        {
            //Allow the user to back out
            string messageBoxText = "Are you sure you want to insert this Technician?";
            string caption = "Insert Technician Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the variables from the form
                string firstName = txtTechFirst.Text;
                string lastName = txtTechLast.Text;
                string email = txtTechEmail.Text;
                string password = txtTechPassword.Text;

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "INSERT INTO Technician (FirstName, LastName, DateAdded, Email, Pass, Position) VALUES (@firstName, @lastName, @dateAdded, @email, @password, @position);";
                comm.Parameters.AddWithValue("@firstName", firstName);
                comm.Parameters.AddWithValue("@lastName", lastName);
                comm.Parameters.Add("@dateAdded", SqlDbType.DateTime).Value = DateTime.Now;
                comm.Parameters.AddWithValue("@email", email);
                comm.Parameters.AddWithValue("@password", password);
                comm.Parameters.AddWithValue("@position", "Tech");

                //Open the connection
                conn.Open();

                //execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Technician Insert Successful.");
                }
                else
                {
                    MessageBox.Show("Error.  No Action Taken.");
                }
            }
            if (result == MessageBoxResult.No | result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("No Action Taken.");
            }
        }

        protected void btnAddLab_Click(object sender, EventArgs e)
        {
            //Allow the user to back out
            string messageBoxText = "Are you sure you want to insert this lab?";
            string caption = "Insert Lab Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the variables from the form
                string labName = txtLabName.Text;
                string school = ddlSchool.SelectedValue;

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "INSERT INTO Lab (Title, School) VALUES (@labName, @school);";
                comm.Parameters.AddWithValue("@labName", labName);
                comm.Parameters.AddWithValue("@school", school);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Lab Insert Successful.");
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

        protected void btnAddHazard_Click(object sender, EventArgs e)
        {
            //Allow the user to back out
            string messageBoxText = "Are you sure you want to insert this hazard?";
            string caption = "Insert Hazard Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the variables from the form
                string hazardName = txtHazard.Text;

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "INSERT INTO Hazard (hazardDescription) VALUES (@hazardName);";
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
                    MessageBox.Show("Hazard Insert Successful.");
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

        protected void btnAddArea_Click(object sender, EventArgs e)
        {
            //Allow the user to back out
            string messageBoxText = "Are you sure you want to insert this Area?";
            string caption = "Insert Area Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the variables from the form
                //Get the lab ID from the dropdown item
                string lab = ddlLab2.SelectedItem.Text;
                var splitValueLab = lab.Split(' ');
                int labID = Convert.ToInt16(splitValueLab[0]);
                string areaName = txtArea.Text;

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "INSERT INTO Area (labID, Title) VALUES (@labID, @areaName);";
                comm.Parameters.AddWithValue("@labID", labID);
                comm.Parameters.AddWithValue("@areaNAme", areaName);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Area Insert Successful.");
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

        protected void btnDeleteTechnician_Click(object sender, EventArgs e)
        {
            //Allow the user to back out
            string messageBoxText = "Are you sure you want to Delete this Technician?";
            string caption = "Delete Technician Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the technician ID from the dropdown item
                string technician = ddlTechnician.SelectedItem.Text;
                var splitValueTech = technician.Split(' ');
                int technicianID = Convert.ToInt16(splitValueTech[3]);
                MessageBox.Show(technicianID.ToString());

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Assign the query to the command
                comm.CommandText = "DELETE FROM Technician WHERE ID = @technicianID";
                comm.Parameters.AddWithValue("@technicianID", technicianID);

                //Open the connection
                conn.Open();

                //Execute the command
                int rows = comm.ExecuteNonQuery();

                //Close the connection
                conn.Close();

                //Message box for success/fail
                if (rows > 0)
                {
                    MessageBox.Show("Technician Deleted.");
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

        protected void btnDeleteLabe_Click(object sender, EventArgs e)
        {
            //Allow the user to back out
            string messageBoxText = "Are you sure you want to delete this lab?";
            string caption = "Lab Technician Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the lab ID from the dropdown item
                string room = ddlLab1.SelectedItem.Text;
                var splitValueLab = room.Split(' ');
                int labID = Convert.ToInt16(splitValueLab[0]);

                //Create a new SQL connection
                SqlConnection conn = new SqlConnection(connectionString);

                //Create a new SQL command
                SqlCommand comm = conn.CreateCommand();

                //Delete child records
                DataLookup.DeleteActionsFromLab(labID);
                DataLookup.DeleteAreaFromLab(labID);


                //Assign the query to the command
                comm.CommandText = "DELETE FROM Lab WHERE ID = @labID";
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
                    MessageBox.Show("Lab Deleted.");
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

        protected void btnDeleteHazard_Click(object sender, EventArgs e)
        {
            //Allow the user to back out
            string messageBoxText = "Are you sure you want to delete this hazard?";
            string caption = "Delete Hazard Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the hazard ID from the dropdown item
                string hazard = ddlHazard.SelectedItem.Text;
                var splitValueHazard = hazard.Split('-');
                int hazardID = Convert.ToInt16(splitValueHazard[0]);

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

        protected void btnDeleteArea_Click(object sender, EventArgs e)
        {
            //Allow the user to back out
            string messageBoxText = "Are you sure you want to delete this area?";
            string caption = "Area Delete Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                //Get the area ID from the dropdown item
                string area = ddlArea.SelectedItem.Text;
                var splitValueArea = area.Split('-');
                int areaID = Convert.ToInt16(splitValueArea[0]);

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

        protected void btnEditTechnician_Click(object sender, EventArgs e)
        {
            //Get the technician ID from the dropdown item
            string technician = ddlTechnician.SelectedItem.Text;
            var splitValueTech = technician.Split(' ');
            int technicianID = Convert.ToInt16(splitValueTech[3]);

            //Redirect to the edit page with the inspection ID
            Response.Redirect("DatabaseAdminTech.aspx?technicianID=" + technicianID);
        }

        protected void btnEditLab_Click(object sender, EventArgs e)
        {
            //Get the lab ID from the dropdown item
            string room = ddlLab1.SelectedItem.Text;
            var splitValueLab = room.Split(' ');
            int labID = Convert.ToInt16(splitValueLab[0]);

            //Redirect to the edit page with the inspection ID
            Response.Redirect("DatabaseAdminLab.aspx?labID=" + labID);
        }

        protected void btnEditHazard_Click(object sender, EventArgs e)
        {
            //Get the hazard ID from the dropdown item
            string hazard = ddlHazard.SelectedItem.Text;
            var splitValueHazard = hazard.Split('-');
            int hazardID = Convert.ToInt16(splitValueHazard[0]);

            //Redirect to the edit page with the inspection ID
            Response.Redirect("DatabaseAdminHazard.aspx?hazardID=" + hazardID);
        }

        protected void btnEditArea_Click(object sender, EventArgs e)
        {
            //Get the area ID from the dropdown item
            string area = ddlArea.SelectedItem.Text;
            var splitValueArea = area.Split('-');
            int areaID = Convert.ToInt16(splitValueArea[0]);

            //Redirect to the edit page with the inspection ID
            Response.Redirect("DatabaseAdminArea.aspx?areaID=" + areaID);
        }
    }
}