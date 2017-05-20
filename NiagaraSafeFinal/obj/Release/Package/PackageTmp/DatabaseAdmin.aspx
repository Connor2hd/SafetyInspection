<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseAdmin.aspx.cs" Inherits="SafetyAuth.DatabaseAdmin" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Niagara Safety | Database Administration</title>

    <!-- Bootstrap -->
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!-- NProgress -->
    <link href="../vendors/nprogress/nprogress.css" rel="stylesheet">

    <!-- Custom Theme Style -->
    <link href="../build/css/custom.min.css" rel="stylesheet">
  </head>

  <body class="nav-md">
    <div class="container body">
      <div class="main_container">
        <div class="col-md-3 left_col">
          <div class="left_col scroll-view">
            <div class="navbar nav_title" style="border: 0;">
              <a href="index.html" class="site_title"><i class="fa fa-cloud"></i> <span>Niagara Safety</span></a>
            </div>

            <div class="clearfix"></div>

            <!-- menu profile quick info -->
            <div class="profile clearfix">
              <div class="profile_pic">
                <img src="images/img.jpg" alt="..." class="img-circle profile_img">
              </div>
              <div class="profile_info">
                <span>Welcome,</span>
                <h2>John Doe</h2>
              </div>
              <div class="clearfix"></div>
            </div>
            <!-- /menu profile quick info -->

            <br />

            <!-- sidebar menu -->
            <div id="sidebar-menu" class="main_menu_side hidden-print main_menu">
              <div class="menu_section">
                <h3>General</h3>
                <ul class="nav side-menu">
                  <li><a href="Homepage.aspx"><i class="fa fa-home"></i> Home </a></li>
                  <li><a href="InspectionSchedule.aspx"><i class="fa fa-calendar"></i> Schedule Inspection </a></li>
                  <li><a href="InspectionSearch.aspx"><i class="fa fa-search"></i> Search Inspection </a></li>
                  <li><a href="InspectionAssigned.aspx"><i class="fa fa-edit"></i> Create Inspection </a></li>
                  <li><a href="CorrectiveActions.aspx"><i class="fa fa-search"></i> Search Actions </a></li>
                  <li><a href="Reports.aspx"><i class="fa fa-bar-chart"></i> Reports </a></li>
                  <li><a href="DatabaseAdmin.aspx"><i class="fa fa-database"></i> Database Admin </a></li>
                  <li><a href="Documentation.aspx"><i class="fa fa-clone"></i> Documentation </a></li>
                </ul>
              </div>
            </div>
            <!-- /sidebar menu -->

            <!-- /menu footer buttons -->
            <div class="sidebar-footer hidden-small">
              <a data-toggle="tooltip" data-placement="top" title="Settings">
                <span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
              </a>
              <a data-toggle="tooltip" data-placement="top" title="FullScreen">
                <span class="glyphicon glyphicon-fullscreen" aria-hidden="true"></span>
              </a>
              <a data-toggle="tooltip" data-placement="top" title="Lock">
                <span class="glyphicon glyphicon-eye-close" aria-hidden="true"></span>
              </a>
              <a data-toggle="tooltip" data-placement="top" title="Logout" href="login.html">
                <span class="glyphicon glyphicon-off" aria-hidden="true"></span>
              </a>
            </div>
            <!-- /menu footer buttons -->
          </div>
        </div>

        <!-- top navigation -->
        <div class="top_nav">
          <div class="nav_menu">
            <nav>
              <div class="nav toggle">
                <a id="menu_toggle"><i class="fa fa-bars"></i></a>
              </div>

              <ul class="nav navbar-nav navbar-right">
                <li class="">
                  <a href="javascript:;" class="user-profile dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                    <img src="images/img.jpg" alt="">John Doe
                    <span class=" fa fa-angle-down"></span>
                  </a>
                  <ul class="dropdown-menu dropdown-usermenu pull-right">
                    <li><a href="javascript:;"> Profile</a></li>
                    <li>
                      <a href="javascript:;">
                        <span class="badge bg-red pull-right">50%</span>
                        <span>Settings</span>
                      </a>
                    </li>
                    <li><a href="javascript:;">Help</a></li>
                    <li><a href="login.html"><i class="fa fa-sign-out pull-right"></i> Log Out</a></li>
                  </ul>
                </li>

                <li role="presentation" class="dropdown">
                  <a href="javascript:;" class="dropdown-toggle info-number" data-toggle="dropdown" aria-expanded="false">
                    <i class="fa fa-envelope-o"></i>
                    <span class="badge bg-green">6</span>
                  </a>
                  <ul id="menu1" class="dropdown-menu list-unstyled msg_list" role="menu">
                    <li>
                      <a>
                        <span class="image"><img src="images/img.jpg" alt="Profile Image" /></span>
                        <span>
                          <span>John Smith</span>
                          <span class="time">3 mins ago</span>
                        </span>
                        <span class="message">
                          Film festivals used to be do-or-die moments for movie makers. They were where...
                        </span>
                      </a>
                    </li>
                    <li>
                      <a>
                        <span class="image"><img src="images/img.jpg" alt="Profile Image" /></span>
                        <span>
                          <span>John Smith</span>
                          <span class="time">3 mins ago</span>
                        </span>
                        <span class="message">
                          Film festivals used to be do-or-die moments for movie makers. They were where...
                        </span>
                      </a>
                    </li>
                    <li>
                      <a>
                        <span class="image"><img src="images/img.jpg" alt="Profile Image" /></span>
                        <span>
                          <span>John Smith</span>
                          <span class="time">3 mins ago</span>
                        </span>
                        <span class="message">
                          Film festivals used to be do-or-die moments for movie makers. They were where...
                        </span>
                      </a>
                    </li>
                    <li>
                      <a>
                        <span class="image"><img src="images/img.jpg" alt="Profile Image" /></span>
                        <span>
                          <span>John Smith</span>
                          <span class="time">3 mins ago</span>
                        </span>
                        <span class="message">
                          Film festivals used to be do-or-die moments for movie makers. They were where...
                        </span>
                      </a>
                    </li>
                    <li>
                      <div class="text-center">
                        <a>
                          <strong>See All Alerts</strong>
                          <i class="fa fa-angle-right"></i>
                        </a>
                      </div>
                    </li>
                  </ul>
                </li>
              </ul>
            </nav>
          </div>
        </div>
        <!-- /top navigation -->

        <!-- page content -->
        <div class="right_col" role="main">
          <div class="">
            <div class="page-title">
              <div class="title_left">
                <h3>Database Administration</h3>
              </div>

              <div class="title_right">
                <div class="col-md-5 col-sm-5 col-xs-12 form-group pull-right top_search">
                </div>
              </div>
            </div>

            <div class="clearfix"></div>
            <form runat="server">

            <!-- instructions -->
            <div class="row">
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Instructions</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                      This is the database administration page.  Below you will find a section for technicians, labs, hazards, and, areas.  
                      Corrective action and inspection management options are found at their respective menu items on the left.
                      Each section has the ability to add or delete an item.  You will be redirected to a secondary page for record editing.
                  </div>
                </div>
              </div>
            </div>

            <!-- Lab Management -->
            <div class="row">
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Lab/Room Management</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <div class="row">
                        Lab Name: 
                        <asp:TextBox ID="txtLabName" runat="server" CssClass="form-control"></asp:TextBox>
                        School:
                        <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control"></asp:DropDownList><br />
                        <asp:Button ID="btnAddLab" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddLab_Click" />
                    </div>
                    <div class="row"><hr />
                        Lab: 
                        <asp:DropDownList ID="ddlLab1" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:Button ID="btnDeleteLab" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDeleteLabe_Click" /><br />
                        <asp:Button ID="btnEditLab" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btnEditLab_Click" />
                    </div>
                    <div class="row">
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- Tech Management -->
            <div class="row">
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Technician Management</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <div class="row">
                        First Name: 
                        <asp:TextBox ID="txtTechFirst" runat="server" CssClass="form-control"></asp:TextBox>
                        Last Name:
                        <asp:TextBox ID="txtTechLast" runat="server" CssClass="form-control"></asp:TextBox>
                        Email:
                        <asp:TextBox ID="txtTechEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        Password:
                        <asp:TextBox ID="txtTechPassword" runat="server" CssClass="form-control"></asp:TextBox><br />
                        <asp:Button ID="btnTechnicianAdd" runat="server" Text="Add" OnClick="btnAddTech_Click" />
                    </div>
                    <div class="row"><hr />
                        Technician: 
                        <asp:DropDownList ID="ddlTechnician" runat="server" CssClass="form-control"></asp:DropDownList><br />
                        <asp:Button ID="btnTechnicianDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDeleteTechnician_Click" />
                        <asp:Button ID="btnTechnicianEdit" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btnEditTechnician_Click" />
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- Area Management -->
            <div class="row">
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Area Management</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <div class="row">
                        Area Name:
                        <asp:TextBox ID="txtArea" runat="server" CssClass="form-control"></asp:TextBox>
                        Lab/Room:
                        <asp:DropDownList ID="ddlLab2" runat="server" CssClass="form-control"></asp:DropDownList><br />
                        <asp:Button ID="btnAddArea" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddArea_Click" />
                    </div>
                    <div class="row"><hr />
                        Area: 
                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control"></asp:DropDownList><br />
                        <asp:Button ID="btnAreaDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDeleteArea_Click" />
                        <asp:Button ID="btnAreaEdit" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btnEditArea_Click" />
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- Hazard Management -->
            <div class="row">
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Hazard Management</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <div class="row">
                        Hazard Name:
                        <asp:TextBox ID="txtHazard" runat="server" CssClass="form-control"></asp:TextBox><br />
                        <asp:Button ID="btnAddHazard" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddHazard_Click" />
                    </div>
                    <div class="row"><hr />
                        Hazard: 
                        <asp:DropDownList ID="ddlHazard" runat="server" CssClass="form-control"></asp:DropDownList><br />
                        <asp:Button ID="btnDeleteHazard" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDeleteHazard_Click" />
                        <asp:Button ID="btnEditHAzard" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btnEditHazard_Click" />
                    </div>
                  </div>
                </div>
              </div>
            </div>


            <div class="clearfix"></div>
            </form>
          </div>
        </div>
        <!-- /page content -->

        <!-- footer content -->
        <footer>
          <div class="pull-right">
            Niagara Safety - Website By <a href="https://niagaracollege.ca">Niagara College</a>
          </div>
          <div class="clearfix"></div>
        </footer>
        <!-- /footer content -->
      </div>
    </div>

    <!-- jQuery -->
    <script src="../vendors/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="../vendors/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- FastClick -->
    <script src="../vendors/fastclick/lib/fastclick.js"></script>
    <!-- NProgress -->
    <script src="../vendors/nprogress/nprogress.js"></script>
    
    <!-- Custom Theme Scripts -->
    <script src="../build/js/custom.min.js"></script>
  </body>
</html>