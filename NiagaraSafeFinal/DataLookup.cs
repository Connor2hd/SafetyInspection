using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Windows;

namespace SafetyAuth
{
    public class DataLookup
    {
        public static string GetEmail(int ID)
        {
            //Create a new SQL connection
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

            //create connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT Email FROM Users WHERE ID=@ID;";
            comm.Parameters.AddWithValue("@ID", ID);

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            string email = "";

            while (reader.Read())
            {
                email = reader.GetString(0);
            }

            //Close the connection
            conn.Close();

            //Return rows
            return email;
        }
        public static void SendMail(int userID, string subject, string body)
        {

            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("in-v3.mailjet.com");
            //mail.From = new MailAddress("ncsafety@niagaracollege.ca");

            //string email = GetEmail(userID);
            //if (userID == 999)
            //{
            //    email = "AdminEmailGoesHere";
            //}
            
            //mail.To.Add(email);
            //mail.Subject = subject;
            //mail.Body = body;

            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("8a174d100e73df0cc9d98913b5e5448c", "7b393802b87ee455998d989250f24fbc");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);
        }

        public static int DeleteActionsFromLab (int labID)
        {
            //Create a new SQL connection
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

            //create connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "DELETE FROM CorrectiveActions WHERE labID = @labID";
            comm.Parameters.AddWithValue("@labID", labID);

            //Open the connection
            conn.Open();

            //Execute the command
            int rows = comm.ExecuteNonQuery();

            //Close the connection
            conn.Close();

            //Return rows
            return rows;
        }
        public static int DeleteAreaFromLab(int labID)
        {
            //Create a new SQL connection
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

            //create connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "DELETE FROM Area WHERE labID = @labID";
            comm.Parameters.AddWithValue("@labID", labID);

            //Open the connection
            conn.Open();

            //Execute the command
            int rows = comm.ExecuteNonQuery();

            //Close the connection
            conn.Close();

            //Return rows
            return rows;
        }

        public static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

        public static string GetLabName(int labID)
        {
            //Blank string to hold the inspectors name
            string name = "";

            //Declare a variable to store the connection string
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

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

            while (reader.Read())
            {
                name = reader.GetString(1);
            }

            //Close the connection
            conn.Close();

            return name;
        }

        public static string GetTechName(int technicianID)
        {
            //Blank string to hold the inspectors name
            string name = "";

            //Declare a variable to store the connection string
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

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

            while (reader.Read())
            {
                name = reader.GetString(1) + " " + reader.GetString(2);
            }

            //Close the connection
            conn.Close();

            return name;
        }

        public static int GetTechnicianID(string technicianName)
        {
            var splitValue = technicianName.Split(' ');
            string techFirst = splitValue[0];
            string techLast = splitValue[1];

            //Declare a variable to hold the connection string
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT ID FROM Technician WHERE FirstName = '" + techFirst + "' AND LastName = '" + techLast + "'";

            //Open the connection
            conn.Open();

            //Execute the command
            int technicianID = comm.ExecuteNonQuery();

            //Close the connection
            conn.Close();

            //Return the value
            return technicianID;
        }

        public static int GetHazardID (string hazardDesc)
        {
            //Declare a variable to hold the connection string
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT ID FROM Hazard WHERE HazardDescription = '" + hazardDesc + "'";

            //Open the connection
            conn.Open();

            //Execute the command
            int hazardID = comm.ExecuteNonQuery();

            //Close the connection
            conn.Close();

            //Return the value
            return hazardID;
        }

        public static int GetAreaID (string areaDesc)
        {
            //Declare a variable to hold the connection string
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT ID FROM Area WHERE Title = '" + areaDesc + "'";

            //Open the connection
            conn.Open();

            //Execute the command
            int areaID = comm.ExecuteNonQuery();

            //Close the connection
            conn.Close();

            //Return the value
            return areaID;
        }

        public static int GetLabID(string labTitle)
        {
            //Declare a variable to hold the connection string
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT ID FROM Lab WHERE Title = '" + labTitle + "'";

            //Open the connection
            conn.Open();

            //Execute the command
            int labID = comm.ExecuteNonQuery();

            //Close the connection
            conn.Close();

            //Return the value
            return labID;
        }

        public static string GetHazardName(int hazardID)
        {
            //Blank string to hold the inspectors name
            string name = "";

            //Declare a variable to store the connection string
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

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
                name = reader.GetString(1);
            }

            //Close the connection
            conn.Close();

            return name;
        }

        public static string GetAreaName(int areaID)
        {
            //Blank string to hold the inspectors name
            string name = "";

            //Declare a variable to store the connection string
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "SELECT * FROM Area WHERE ID = " + areaID;

            //Open the connection
            conn.Open();

            //Assign the command to a reader and execute
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                name = reader.GetString(2);
            }

            //Close the connection
            conn.Close();

            return name;
        }

        public static int InsertAction(int inspectionID, int labID, int technicianID, int hazardID, int areaID, string hazardDesc, string actionDesc, DateTime dueDate, string hazardOther, string areaOther)
        {
            //Declare a variable to store the connection string
            string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

            //Create a new SQL connection
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a new SQL command
            SqlCommand comm = conn.CreateCommand();

            //Assign the query to the command
            comm.CommandText = "INSERT INTO CorrectiveActions (InspectionID, LabID, TechnicianID, HazardID, AreaID, DetailDescription, ActionDescription, DueDate, HazardOther, AreaOther) VALUES (@inspectionID, @labID, @technicianID, @hazardID, @areaID, @hazardDesc, @actionDesc, @dueDate, @hazardOther, @areaOther);";

            //Fill in the variables
            comm.Parameters.AddWithValue("@inspectionID", inspectionID);
            comm.Parameters.AddWithValue("@labID", labID);
            comm.Parameters.AddWithValue("@technicianID", technicianID);
            comm.Parameters.AddWithValue("@hazardID", hazardID);
            comm.Parameters.AddWithValue("@areaID", areaID);
            comm.Parameters.AddWithValue("@hazardDesc", hazardDesc);
            comm.Parameters.AddWithValue("@actionDesc", actionDesc);
            comm.Parameters.AddWithValue("@dueDate", dueDate);
            comm.Parameters.AddWithValue("@hazardOther", hazardOther);
            comm.Parameters.AddWithValue("@areaOther", areaOther);

            //Open the connection
            conn.Open();

            //Execute the command
            int rows = comm.ExecuteNonQuery();

            return rows;
        }
    }
}