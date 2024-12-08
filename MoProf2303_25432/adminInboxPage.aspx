<%@ Page Title="" Language="C#" MasterPageFile="~/adminDashboard.Master" AutoEventWireup="true" CodeBehind="adminInboxPage.aspx.cs" Inherits="MoProf2303_25432.adminInboxPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <!-- Approvals Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-light rounded align-items-center justify-content-start mx-0">
            <div class="col-md-12 text-center">
                <h3>Approvals</h3>
                <asp:Repeater ID="rptApprovals" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Username</th>
                                    <th>Email</th>
                                    <th>Type</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Username") %></td>
                            <td><%# Eval("Email") %></td>
                            <td><%# Eval("UserType") %></td>
                            <td>
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="btn btn-success" CommandArgument='<%# Eval("User_Id") + "," + Eval("UserType") %>' OnClick="btnAccept_Click" />
                                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger" CommandArgument='<%# Eval("User_Id") + "," + Eval("UserType") %>' OnClick="btnReject_Click" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <!-- Approvals End -->
</asp:Content>

