<%@ Page Title="" Language="C#" MasterPageFile="~/stDashboard.Master" AutoEventWireup="true" CodeBehind="tutorViewStudentsPage.aspx.cs" Inherits="MoProf2303_25432.tutorViewStudentsPage" %>
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
            max-width: 600px;
            margin: 2rem auto;
        }
        .edit-profile-container input,
        .edit-profile-container select,
        .edit-profile-container textarea,
        .edit-profile-container input[type="password"] {
            border: 1px solid #ccc;
            background: transparent;
            height: 2.5rem;
            width: 100%;
            padding: 0 0.75rem;
            margin-top: 0.5rem;
            border-radius: 8px;
            font-size: 1rem;
            margin-bottom: 1rem;
        }
        .edit-profile-container label {
            margin-top: 1rem;
            font-weight: bold;
            font-size: 1rem;
            color: #333;
        }
        .edit-profile-container .btn {
            background: #007bff;
            border: none;
            border-radius: 8px;
            padding: 0.75rem 1.5rem;
            color: #fff;
            cursor: pointer;
            margin-top: 1rem;
            font-size: 1rem;
            transition: background 0.3s;
        }
        .edit-profile-container .btn:hover {
            background: #0056b3;
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
            <h1>Payments Approval</h1>
        </div>
        <div class="edit-profile-container">
            <asp:GridView ID="gvPayments" runat="server" AutoGenerateColumns="False" OnRowCommand="gvPayments_RowCommand" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="Booking_Id" HeaderText="Booking ID" />
                    <asp:BoundField DataField="Student_Id" HeaderText="Student ID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Course_Id" HeaderText="Course ID" />
                    <asp:BoundField DataField="Subject_Name" HeaderText="Subject Name" />
                    <asp:BoundField DataField="Payment" HeaderText="Payment" />
                    <asp:ButtonField CommandName="ApprovePayment" ButtonType="Button" Text="Approve" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />
        </div>
    </main>
</asp:Content>
