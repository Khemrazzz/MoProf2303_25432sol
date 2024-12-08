<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMasterPage.Master" AutoEventWireup="true" CodeBehind="studentPayStsPage.aspx.cs" Inherits="MoProf2303_25432.studentPayStsPage" %>
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

       .pending-payments-container {
           display: flex;
           flex-direction: column;
           justify-content: center;
           border-radius: 12px;
           padding: 2rem;
           background-color: #ffffff;
           box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
           width: 100%;
           max-width: 800px;
           margin: 2rem auto;
       }

       .pending-payments-container h1 {
           font-size: 2rem;
           color: #333;
           margin-bottom: 1rem;
       }

       .pending-payments-container .table {
           width: 100%;
           border-collapse: collapse;
           margin-top: 1rem;
       }

       .pending-payments-container .table th,
       .pending-payments-container .table td {
           border: 1px solid #ddd;
           padding: 0.75rem;
           text-align: left;
       }

       .pending-payments-container .table th {
           background-color: #007bff;
           color: white;
           font-weight: bold;
       }

       .pending-payments-container .table td {
           background-color: #f9f9f9;
       }

       .pending-payments-container .btn {
           background: linear-gradient(135deg, #007bff, #0056b3);
           border: none;
           border-radius: 8px;
           padding: 0.5rem 1rem;
           color: #fff;
           cursor: pointer;
           font-size: 1rem;
           transition: all 0.3s;
           box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
       }

       .pending-payments-container .btn:hover {
           background: linear-gradient(135deg, #0056b3, #003f7f);
           box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
       }

       .text-center {
           text-align: center;
       }

       .text-primary {
           color: #007bff;
       }
   </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

<main class="main">
       <div class="pending-payments-container">
           <div class="text-center mb-5">
               <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">TUTOR</h5>
               <h1>Pending Payments</h1>
           </div>

      <asp:GridView ID="gvPendingPayments" runat="server" AutoGenerateColumns="False" CssClass="table" DataKeyNames="Course_Id" OnRowCommand="gvPendingPayments_RowCommand">
    <Columns>
        <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
        <asp:BoundField DataField="TutorName" HeaderText="Tutor Name" />
        <asp:BoundField DataField="Fees" HeaderText="Fees" DataFormatString="{0:C}" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnPayNow" runat="server" Text="Pay Now" CssClass="btn" CommandName="PayNow" CommandArgument='<%# Eval("Course_Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

       </div>
   </main>

</asp:Content>