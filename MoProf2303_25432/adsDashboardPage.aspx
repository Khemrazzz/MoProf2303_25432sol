<%@ Page Title="" Language="C#" MasterPageFile="~/advertiserMasterPage.Master" AutoEventWireup="true" CodeBehind="adsDashboardPage.aspx.cs" Inherits="MoProf2303_25432.adsDashboardPage" %>
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
        </main>

       
</asp:Content>
