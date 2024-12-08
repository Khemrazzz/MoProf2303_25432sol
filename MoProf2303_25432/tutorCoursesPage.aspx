<%@ Page Title="" Language="C#" MasterPageFile="~/stDashboard.Master" AutoEventWireup="true" CodeBehind="tutorCoursesPage.aspx.cs" Inherits="MoProf2303_25432.tutorCoursesPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style>
        .main {
            background-color: #f0f4f8;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 2rem;
        }

        .edit-profile-container {
            display: flex;
            flex-direction: column;
            justify-content: center;
            border-radius: 12px;
            padding: 2rem;
            background-color: #ffffff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 800px; /* Increased width */
            margin: 2rem auto;
        }

        .course-item {
            margin-bottom: 2rem;
            padding: 1.5rem;
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            background-color: #fff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .course-details {
            margin-bottom: 1rem;
        }

        .course-details h3 {
            font-size: 1.5rem;
            margin-bottom: 0.5rem;
        }

        .course-details span {
            display: block;
            margin-bottom: 0.25rem;
            font-size: 1rem;
        }

        .btn-container {
            display: flex;
            justify-content: space-between;
            margin-top: 1rem;
        }

        .btn-edit, .btn-remove {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 0.5rem 1rem;
            border-radius: 8px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn-remove {
            background-color: #dc3545;
        }

        .btn-edit:hover {
            background-color: #0056b3;
        }

        .btn-remove:hover {
            background-color: #c82333;
        }

        .text-center {
            text-align: center;
        }

        .text-primary {
            color: #007bff;
        }

        .vertical-header .navbar {
            display: flex;
            flex-direction: column;
            width: 200px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <main>
        <div class="text-center mb-5">
            <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">TUTOR</h5>
            <h1>My Courses</h1>
        </div>
        <div class="edit-profile-container">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <asp:Repeater ID="rptCourses" runat="server">
                <ItemTemplate>
                    <div class="course-item">
                        <div class="course-details">
                            <h3><%# Eval("Subject_Name") %></h3>
                            <span>Category: <%# Eval("Category") %></span>
                            <span>Description: <%# Eval("Description") %></span>
                            <span>Fees: <%# Eval("Fees", "{0:C}") %></span>
                            <span>Date: <%# Eval("Date", "{0:MM/dd/yyyy}") %></span>
                            <span>Day: <%# Eval("DayName") %></span>
                            <span>Time: <%# Eval("TimeSlot") %></span>
                            <span>Mode: <%# Eval("ModeName") %></span>
                            <span>Location: <%# Eval("StrAddress") %>, <%# Eval("VillageTown") %>, <%# Eval("District") %></span>
                            <span>Seats Available: <%# Eval("SeatCount") %></span>
                        </div>
                        <div class="btn-container">
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-edit" CommandArgument='<%# Eval("Course_Id") %>' OnClick="btnEdit_Click" />
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn-remove" CommandArgument='<%# Eval("Course_Id") %>' OnClientClick="return confirmRemove();" OnClick="btnRemove_Click" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </main>
</asp:Content>
