using Microsoft.AspNet.Identity.Owin;
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
    public partial class Login : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Technician WHERE Email=@email AND Pass=@password";
            comm.Parameters.AddWithValue("@email", txtEmail.Text);
            comm.Parameters.AddWithValue("@password", txtPassword.Text);

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            string role = "";
            string firstName = "";
            string lastName = "";
            int technicianID = 0;

            while (reader.Read())
            {
                role = reader.GetString(6);
                firstName = reader.GetString(1);
                lastName = reader.GetString(2);
                technicianID = reader.GetInt32(0);

                if (role.Length > 1)
                {
                    Globals.role = role;
                    Globals.firstName = firstName;
                    Globals.lastName = lastName;
                    Globals.technicianID = technicianID;
                }
            }

            if (role.Length > 0)
            {
                Response.Redirect("Homepage.aspx");
            }

            if (role.Length < 1)
            {
                lblStatus.Text = "Invalid Email and/or Password Combination.";
            }

            //Close the connection
            conn.Close();
        }
    }
}