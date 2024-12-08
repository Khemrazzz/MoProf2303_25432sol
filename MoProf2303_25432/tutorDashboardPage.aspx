<%@ Page Title="" Language="C#" MasterPageFile="~/stDashboard.Master" AutoEventWireup="true" CodeBehind="tutorDashboardPage.aspx.cs" Inherits="MoProf2303_25432.tutorDashboardPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style>
        .text-primary {
            color: #007bff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

    
    
        <main>
            <h1>My Courses</h1>
            <div class="subjects">
                <div class="eg">
                    <span class="material-icons-sharp">architecture</span>
                    <h3>Engineering Graphics</h3>
                    <h2>12/14</h2>
                    <div class="progress">
                        <svg>
                            <circle cx="38" cy="38" r="36"></circle></svg>
                        <div class="number">
                            <p>86%</p>
                        </div>
                    </div>
                    <small class="text-muted">Last 24 Hours</small>
                </div>
                <div class="mth">
                    <span class="material-icons-sharp">functions</span>
                    <h3>Mathematical Engineering</h3>
                    <h2>27/29</h2>
                    <div class="progress">
                        <svg>
                            <circle cx="38" cy="38" r="36"></circle></svg>
                        <div class="number">
                            <p>93%</p>
                        </div>
                    </div>
                    <small class="text-muted">Last 24 Hours</small>
                </div>
                <div class="cs">
                    <span class="material-icons-sharp">computer</span>
                    <h3>Computer Architecture</h3>
                    <h2>27/30</h2>
                    <div class="progress">
                        <svg>
                            <circle cx="38" cy="38" r="36"></circle></svg>
                        <div class="number">
                            <p>81%</p>
                        </div>
                    </div>
                    <small class="text-muted">Last 24 Hours</small>
                </div>
                <div class="cg">
                    <span class="material-icons-sharp">dns</span>
                    <h3>Database Management</h3>
                    <h2>24/25</h2>
                    <div class="progress">
                        <svg>
                            <circle cx="38" cy="38" r="36"></circle></svg>
                        <div class="number">
                            <p>96%</p>
                        </div>
                    </div>
                    <small class="text-muted">Last 24 Hours</small>
                </div>
                <div class="net">
                    <span class="material-icons-sharp">router</span>
                    <h3>Network Security</h3>
                    <h2>25/27</h2>
                    <div class="progress">
                        <svg>
                            <circle cx="38" cy="38" r="36"></circle></svg>
                        <div class="number">
                            <p>92%</p>
                        </div>
                    </div>
                    <small class="text-muted">Last 24 Hours</small>
                </div>
            </div>
            <div class="timetable" id="timetable">
                <div>
                    <span id="prevDay">&lt;</span>
                    <h2>Recently online</h2>
                    <span id="nextDay">&gt;</span>
                </div>
                <span class="closeBtn" onclick="timeTableAll()">X</span>
                <table>
                    <thead>
                        <tr>
                            <th>Time</th>
                            <th>Room No.</th>
                            <th>Subject</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <ajaxToolkit:PieChart ID="PieChart1" runat="server"></ajaxToolkit:PieChart>
            </div>

        </main>

        


 

</asp:Content>
