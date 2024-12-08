<%@ Page Title="" Language="C#" MasterPageFile="~/adminDashboard.Master" AutoEventWireup="true" CodeBehind="adminAuditPage.aspx.cs" Inherits="MoProf2303_25432.adminAuditPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

    <!-- Blank Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row vh-100 bg-light rounded align-items-center justify-content-start mx-0">
            <div class="col-md-12 text-center">
                <h3>User Login Attempts</h3>
                
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mb-3" placeholder="Search by username..." AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                            <asp:GridView ID="gvLoginAttempts" runat="server" AutoGenerateColumns="False" 
                                OnRowCommand="gvLoginAttempts_RowCommand" CssClass="table table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="failedLoginAttempts" HeaderText="Failed Login Attempts" />
                                    <asp:BoundField DataField="Username" HeaderText="Username" />
                                    <asp:BoundField DataField="AttemptTime" HeaderText="Attempt Time" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                                    <asp:BoundField DataField="IPAddress" HeaderText="IP Address" />
                                    <asp:BoundField DataField="LoginType" HeaderText="Login Type" />
                                    <asp:BoundField DataField="IsSuccessful" HeaderText="Is Successful" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="btnUnblock" runat="server" CommandName="Unblock" CommandArgument="<%# Container.DataItemIndex %>" Text="Unblock" CssClass="btn btn-primary" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="alert alert-warning" role="alert">
                                        No login attempts found.
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="gvLoginAttempts" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                
            </div>
        </div>
    </div>
    <!-- Blank End -->

</asp:Content>
