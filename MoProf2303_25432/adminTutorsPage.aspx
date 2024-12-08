<%@ Page Title="" Language="C#" MasterPageFile="~/adminDashboard.Master" AutoEventWireup="true" CodeBehind="adminTutorsPage.aspx.cs" Inherits="MoProf2303_25432.adminTutorsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

    <!-- Tutors List Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-light rounded align-items-start justify-content-center mx-0">
            <div class="col-md-12 text-center">
                <h3>List of All Tutors</h3>
                
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mb-3" placeholder="Search by username..." AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                            <asp:GridView ID="gvTutors" runat="server" AutoGenerateColumns="False" 
                                OnRowCommand="gvTutors_RowCommand" CssClass="table table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="Tutor_Id" HeaderText="Tutor ID" />
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                    <asp:BoundField DataField="Username" HeaderText="Username" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="btnToggleActive" runat="server" CommandName="ToggleActive" CommandArgument="<%# Container.DataItemIndex %>" Text='<%# Eval("IsActive") != DBNull.Value && Convert.ToBoolean(Eval("IsActive")) ? "Freeze" : "Unfreeze" %>' CssClass="btn btn-primary" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="alert alert-warning" role="alert">
                                        No tutors found.
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="gvTutors" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                
            </div>
        </div>
    </div>
    <!-- Tutors List End -->

</asp:Content>
