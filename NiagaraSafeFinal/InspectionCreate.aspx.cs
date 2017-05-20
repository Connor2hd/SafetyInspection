using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SafetyAuth
{
    public partial class InspectionCreate : System.Web.UI.Page
    {
        //Declare the connection string for this page
        string connectionString = DataLookup.GetConnectionStringByName("AzureServer");

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    Response.Redirect("Login.aspx");
            //}

            //Set the initial amount of areas and hazards
            int areaCount = 1;

            //Check if the viewstate with the area count already exists
            if (ViewState["areaCount"] != null)
            {
                //Convert the view state back to an int
                areaCount = Convert.ToInt32(ViewState["areaCount"]);
            }
            else
            {
                ViewState["areaCount"] = areaCount;
            }

            //Create the required number of areas
            for (int i = 2; i <= areaCount; i++)
            {
                createArea(i);
            }
        }

        protected void btnCreateArea_Click(object sender, EventArgs e)
        {
            //Get the current number of areas
            int areaCount = Convert.ToInt32(ViewState["areaCount"]) + 1;

            //Create the area
            createArea(areaCount);

            //Set the new area into the viewstate
            ViewState["areaCount"] = areaCount;
        }

        private void createArea(int areaCount)
        {
            //Start generating the HTML for the start of the box element
            pnlArea.Controls.Add(new LiteralControl("<div class='row'><div class='col-md-12 col-sm-12 col-xs-12'><div class='x_panel'><div class='x_title'><div class='row'><div class='col-md-3'><h2>Area: "));

            //Create the title dropdown
            DropDownList ddlArea = new DropDownList();
            ddlArea.ID = "ddArea" + areaCount;
            ddlArea.CssClass = "form-control";
            pnlArea.Controls.Add(ddlArea);

            //This block of code populates the area drop down
            string connectionString = "Server=tcp:niagarasafety.database.windows.net,1433;Initial Catalog=NiagaraSafety;Persist Security Info=False;User ID=connor2hd;Password=Safety123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT Title FROM Area";
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                ddlArea.Items.Add(reader.GetString(0));
            }
            conn.Close();

            pnlArea.Controls.Add(new LiteralControl("</h2></div><div class='col-md-3'><h2>Other: "));

            //Create the other textbox for the title
            TextBox txtOther = new TextBox();
            txtOther.ID = "txtOther" + areaCount;
            txtOther.CssClass = "form-control";
            pnlArea.Controls.Add(txtOther);

            //HTML for the element styling and such
            pnlArea.Controls.Add(new LiteralControl("</h2></div></div><div class='clearfix'></div></div><div class='x_content'><div class='row'><div class='col-lg-2'>Hazard: "));

            //Add the hazard drop down list
            DropDownList ddlHazard = new DropDownList();
            ddlHazard.ID = "ddlHazard" + areaCount;
            ddlHazard.CssClass = "form-control";
            pnlArea.Controls.Add(ddlHazard);

            //This block of code populates the hazard drop down
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand comm2 = conn2.CreateCommand();
            comm2.CommandText = "SELECT HazardDescription FROM Hazard";
            conn2.Open();
            SqlDataReader reader2 = comm2.ExecuteReader();
            while (reader2.Read())
            {
                ddlHazard.Items.Add(reader2.GetString(0));
            }
            conn2.Close();

            //Add the other hazard textbox
            pnlArea.Controls.Add(new LiteralControl("</div><div class='col-lg-2'>Hazard Other: "));
            TextBox txtHazardOther = new TextBox();
            txtHazardOther.ID = "txtHazardOther" + areaCount;
            txtHazardOther.CssClass = "form-control";
            pnlArea.Controls.Add(txtHazardOther);

            //Add the hazard description textbox
            pnlArea.Controls.Add(new LiteralControl("</div><div class='col-lg-3'>Hazard Description: "));
            TextBox txtHazard = new TextBox();
            txtHazard.ID = "txtHazard" + areaCount;
            txtHazard.CssClass = "form-control";
            pnlArea.Controls.Add(txtHazard);

            //Add the corrective action description
            pnlArea.Controls.Add(new LiteralControl("</div><div class='col-lg-3'>Corrective Action Description: "));
            TextBox txtActionDesc = new TextBox();
            txtActionDesc.ID = "txtActionDesc" + areaCount;
            txtActionDesc.CssClass = "form-control";
            pnlArea.Controls.Add(txtActionDesc);

            //Add the corrective action due date
            pnlArea.Controls.Add(new LiteralControl("</div><div class='col-lg-2'>Corrective Action Due Date: <br>"));
            Calendar calDueDate = new Calendar();
            calDueDate.ID = "calDueDate" + areaCount;
            pnlArea.Controls.Add(calDueDate);

            //End all the div tags
            pnlArea.Controls.Add(new LiteralControl("</div></div></div></div></div></div>"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ////Current idea is to build an array for each box type, so all of the hazardDesc go into one array.  Then somehow
            ////At a later time Get the first item of each array and build a string from that

            ////List Decleration
            //List<string> area = new List<string>();
            //List<string> areaOther = new List<string>();
            //List<string> hazard = new List<string>();
            //List<string> hazardOther = new List<string>();
            //List<string> hazardDesc = new List<string>();
            //List<string> actionDesc = new List<string>();
            //List<DateTime> dueDate = new List<DateTime>();

            //foreach (Control c in pnlArea.Controls)
            //{
            //    if (c.ID.Contains("ddlArea"))
            //    {
            //        area.Add(((DropDownList)c).SelectedValue);
            //    }
            //    if (c.ID.Contains("txtArea"))
            //    {
            //        areaOther.Add(((TextBox)c).Text);
            //    }
            //    if (c.ID.Contains("ddHazard"))
            //    {
            //        hazard.Add(((DropDownList)c).SelectedValue);
            //    }
            //    if (c.ID.Contains("txtHazard"))
            //    {
            //        hazardOther.Add(((TextBox)c).Text);
            //    }
            //    if (c.ID.Contains("txtHazardDesc"))
            //    {
            //        hazardDesc.Add(((TextBox)c).Text);
            //    }
            //    if (c.ID.Contains("txtActionDesc"))
            //    {
            //        actionDesc.Add(((TextBox)c).Text);
            //    }
            //    if (c.ID.Contains("calDueDate"))
            //    {
            //        dueDate.Add(((Calendar)c).SelectedDate);
            //    }
            //}

            //int areaCount = Convert.ToInt32(ViewState["areaCount"]);
            //for (int i = 0; i < areaCount; i++)
            //{
            //    area[i] = ((TextBox)pnlArea.Controls["TxtArea" + (i + 1).ToString()]).Text;
            //}

            //string test = area[0] + " " + areaOther[0] + " " + hazard[0] + " " + hazardOther[0] + " " + hazardDesc[0] + " " + actionDesc[0] + " " + (dueDate[0].ToString());

            //MessageBox.Show(test);



        //}
    }
    }
}