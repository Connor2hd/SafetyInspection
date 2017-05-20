<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActionEdit.aspx.cs" Inherits="SafetyAuth.ActionEdit" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Niagara Safety | Corrective Action Edit</title>

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
                <h3>Corrective Action #<asp:Label ID="lblBigID" runat="server" Text="Label"></asp:Label></h3>
              </div>
            </div>
            <div class="clearfix"></div>
            <form runat="server">

            <!-- Main Data -->
            <div class="row">
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Main Information</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                      <div class="row">
                          <div class="col-md-4">
                            Inspection Number: <asp:TextBox ID="txtInspectionID" CssClass="form-control" runat="server"></asp:TextBox>
                          </div>
                          <div class="col-md-4">
                            Technician ID: <asp:DropDownList ID="ddlTechnician" CssClass="form-control" runat="server"></asp:DropDownList>
                          </div>
                          <div class="col-md-4">
                            Room ID: <asp:DropDownList ID="ddlRoom" CssClass="form-control" runat="server"></asp:DropDownList>
                          </div>
                      </div>
                      <div class="row">
                          <div class="col-md-4">
                              Action Status: <asp:DropDownList ID="ddlInspectionStatus" CssClass="form-control" runat="server"></asp:DropDownList>
                          </div>
                          <div class="col-md-4">
<%--                              Technician Name: <asp:Label ID="lblTechnicianName" runat="server" Text="Label"></asp:Label>--%>
                          </div>
                          <div class="col-md-4">
<%--                              Room Name: <asp:Label ID="lblRoomName" runat="server" Text="Label"></asp:Label>--%>
                          </div>
                      </div>
                      <div class="row">
                          <div class="col-lg-3">
                              Area: <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control"></asp:DropDownList>
                          </div>
                          <div class="col-lg-9">
                              Area Other: <asp:TextBox ID="txtArea" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="row">
                          <div class="col-lg-3">
                              Hazard: <asp:DropDownList ID="ddlHazard" runat="server" CssClass="form-control"></asp:DropDownList>
                          </div>
                          <div class="col-lg-9">
                              Hazard Other: <asp:TextBox ID="txtHazard" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="row">
                          <div class="col-lg-6">
                              Detail Description: <asp:TextBox ID="txtDetailDesc" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <div class="col-lg-6">
                              Action Description: <asp:TextBox ID="txtActionDesc" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <hr />
                      <div class="row">
                          <div class="col-md-4">
                              Due Date: <br />
                              <asp:Calendar ID="calDueDate" runat="server"></asp:Calendar>
                          </div>
                      </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="clearfix"></div>

            <!-- Management Actions -->
            <div class="row">
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Management Actions</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                      <asp:Button ID="btnSave" runat="server" Text="Save Corrective Action" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />
                      <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-block" OnClick="btnCancel_Click" />
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
