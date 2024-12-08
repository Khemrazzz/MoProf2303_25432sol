<%@ Page Title="" Language="C#" MasterPageFile="~/adminDashboard.Master" AutoEventWireup="true" CodeBehind="adminEditCatPage.aspx.cs" Inherits="MoProf2303_25432.adminEditCatPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style>
        /* General Styles */
        body {
            font-family: 'Arial', sans-serif;
            background-color: #fff;
            margin: 0;
            padding: 0;
        }

        .container {
            padding: 20px;
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
        }

        .header-container {
            display: flex;
            align-items: center;
            justify-content: center;
            margin-bottom: 20px;
            width: 100%;
            flex-direction: column;
        }

        .header-container h1 {
            text-align: center;
            margin: 0;
        }

        .message {
            margin-bottom: 20px;
            padding: 10px;
            color: #155724;
            background-color: #d4edda;
            border: 1px solid #c3e6cb;
            border-radius: 5px;
            width: 100%;
            max-width: 800px;
            text-align: center;
            display: none;
        }

        .buttons-container {
            display: flex;
            justify-content: center;
            margin-bottom: 20px;
            width: 100%;
        }

        .buttons-container .btn {
            background-color: #fd7e14;
            color: #f4f4f4;
            border: none;
            border-radius: 8px;
            padding: 10px 20px;
            cursor: pointer;
            font-size: 1rem;
            transition: background-color 0.3s ease, color 0.3s ease;
            margin: 0 10px;
        }

        .buttons-container .btn:hover {
            background-color: #fd7e14;
            color: #f4f4f4;
        }

        .table-container {
            background-color: #f4f4f4;
            border-radius: 12px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            padding: 20px;
            margin: 10px 0;
            width: 100%;
            max-width: 800px;
        }

        .table-container h2 {
            margin-top: 0;
            text-align: center;
        }

        .table-container table {
            width: 100%;
            border-collapse: collapse;
        }

        .table-container table, th, td {
            border: 1px solid #ddd;
        }

        .table-container th, td {
            padding: 8px;
            text-align: left;
        }

        .table-container th {
            background-color: #f2f2f2;
        }

        .table-container .btn-add {
            background-color: #fd7e14;
            color: #f4f4f4;
            border: none;
            border-radius: 8px;
            padding: 10px 20px;
            cursor: pointer;
            font-size: 1rem;
            transition: background-color 0.3s ease, color 0.3s ease;
            margin-bottom: 10px;
        }

        .table-container .btn-add:hover {
            background-color: #fd7e14;
            color: #f4f4f4;
        }

        .table-container .actions {
            display: flex;
            justify-content: flex-start;
            gap: 10px;
        }

        .btn-edit {
            background-color: #fd7e14;
            color: #f4f4f4;
            border: none;
            border-radius: 8px;
            padding: 10px 20px;
            cursor: pointer;
            font-size: 1rem;
            transition: background-color 0.3s ease, color 0.3s ease;
        }

        .btn-edit:hover {
            background-color: #fd7e14;
            color: #f4f4f4;
        }

        .btn-remove {
            background-color: transparent;
            color: #ff0000;
            border: 2px solid #ff0000;
            border-radius: 8px;
            padding: 10px 20px;
            cursor: pointer;
            font-size: 1rem;
            transition: background-color 0.3s ease, color 0.3s ease;
        }

        .btn-remove:hover {
            background-color: #ff0000;
            color: #f4f4f4;
        }

        .inline-controls {
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .paging-buttons {
            display: flex;
            justify-content: center;
            margin-top: 10px;
            gap: 10px;
        }

        .paging-buttons .btn-paging {
            background-color: #fd7e14;
            color: #f4f4f4;
            border: none;
            border-radius: 8px;
            padding: 10px 20px;
            cursor: pointer;
            font-size: 1rem;
        }

        .paging-buttons .btn-paging:hover {
            background-color: #e67e22;
            color: #f4f4f4;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div class="container">
        <div class="header-container">
            <h1>Edit Categories</h1>
            <asp:Label ID="lblMsg" runat="server" CssClass="message"></asp:Label>
        </div>
        <div class="buttons-container">
            <asp:Button ID="btnEditSubjects" runat="server" Text="Edit Subjects" CssClass="btn btn-add" OnClick="btnEditSubjects_Click" />
            <asp:Button ID="btnEditGrades" runat="server" Text="Edit Grades" CssClass="btn btn-add" OnClick="btnEditGrades_Click" />
            <asp:Button ID="btnEditCities" runat="server" Text="Edit Cities" CssClass="btn btn-add" OnClick="btnEditCities_Click" />
            <asp:Button ID="btnEditDistricts" runat="server" Text="Edit Districts" CssClass="btn btn-add" OnClick="btnEditDistricts_Click" />
        </div>
        <div class="table-container">
            <!-- Edit Subjects Table -->
            <asp:Panel ID="pnlEditSubjects" runat="server" Visible="false">
                <div class="inline-controls">
                    <asp:TextBox ID="txtEditSubjectName" runat="server" CssClass="form-control search-box" Placeholder="Category Name"></asp:TextBox>
                    <asp:FileUpload ID="fuEditSubjectImage" runat="server" CssClass="form-control search-box" />
                    <asp:Button ID="btnSaveSubject" runat="server" Text="Save Subject" CssClass="btn btn-add" OnClick="btnSaveSubject_Click" />
                    <asp:Button ID="btnCancelSubject" runat="server" Text="Cancel" CssClass="btn btn-add" OnClick="btnCancelSubject_Click" />
                </div>
                <asp:GridView ID="gvEditSubjects" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvEditSubjects_PageIndexChanging" OnRowCommand="gvEditSubjects_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Category_Id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="Category_Name" HeaderText="Category Name" />
                        <asp:TemplateField HeaderText="Category Image">
                            <ItemTemplate>
                                <asp:Image ID="imgEditSubject" runat="server" ImageUrl='<%# Eval("subject_image") %>' Width="100px" Height="100px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="actions">
                                    <asp:Button ID="btnEditSubject" runat="server" CommandName="EditSubject" Text="Edit" CommandArgument='<%# Eval("Category_Id") %>' CssClass="btn btn-edit" />
                                    <asp:Button ID="btnRemoveSubject" runat="server" CommandName="RemoveSubject" Text="Remove" CommandArgument='<%# Eval("Category_Id") %>' CssClass="btn btn-remove" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="paging-buttons">
                    <asp:Button ID="btnPreviousSubject" runat="server" Text="Previous" CssClass="btn-paging" OnClick="btnPreviousSubject_Click" />
                    <asp:Button ID="btnNextSubject" runat="server" Text="Next" CssClass="btn-paging" OnClick="btnNextSubject_Click" />
                </div>
            </asp:Panel>
            <!-- Edit Grades Table -->
            <asp:Panel ID="pnlEditGrades" runat="server" Visible="false">
                <div class="inline-controls">
                    <asp:TextBox ID="txtEditGradeName" runat="server" CssClass="form-control search-box" Placeholder="Grade Name"></asp:TextBox>
                    <asp:Button ID="btnSaveGrade" runat="server" Text="Save Grade" CssClass="btn btn-add" OnClick="btnSaveGrade_Click" />
                    <asp:Button ID="btnCancelGrade" runat="server" Text="Cancel" CssClass="btn btn-add" OnClick="btnCancelGrade_Click" />
                </div>
                <asp:GridView ID="gvEditGrades" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvEditGrades_PageIndexChanging" OnRowCommand="gvEditGrades_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Grade_Id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="Grade_Name" HeaderText="Grade Name" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="actions">
                                    <asp:Button ID="btnEditGrade" runat="server" CommandName="EditGrade" Text="Edit" CommandArgument='<%# Eval("Grade_Id") %>' CssClass="btn btn-edit" />
                                    <asp:Button ID="btnRemoveGrade" runat="server" CommandName="RemoveGrade" Text="Remove" CommandArgument='<%# Eval("Grade_Id") %>' CssClass="btn btn-remove" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="paging-buttons">
                    <asp:Button ID="btnPreviousGrade" runat="server" Text="Previous" CssClass="btn-paging" OnClick="btnPreviousGrade_Click" />
                    <asp:Button ID="btnNextGrade" runat="server" Text="Next" CssClass="btn-paging" OnClick="btnNextGrade_Click" />
                </div>
            </asp:Panel>
            <!-- Edit Cities Table -->
            <asp:Panel ID="pnlEditCities" runat="server" Visible="false">
                <div class="inline-controls">
                    <asp:TextBox ID="txtEditCityName" runat="server" CssClass="form-control search-box" Placeholder="City Name"></asp:TextBox>
                    <asp:Button ID="btnSaveCity" runat="server" Text="Save City" CssClass="btn btn-add" OnClick="btnSaveCity_Click" />
                    <asp:Button ID="btnCancelCity" runat="server" Text="Cancel" CssClass="btn btn-add" OnClick="btnCancelCity_Click" />
                </div>
                <asp:GridView ID="gvEditCities" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvEditCities_PageIndexChanging" OnRowCommand="gvEditCities_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="CityID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="CityName" HeaderText="City Name" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="actions">
                                    <asp:Button ID="btnEditCity" runat="server" CommandName="EditCity" Text="Edit" CommandArgument='<%# Eval("CityID") %>' CssClass="btn btn-edit" />
                                    <asp:Button ID="btnRemoveCity" runat="server" CommandName="RemoveCity" Text="Remove" CommandArgument='<%# Eval("CityID") %>' CssClass="btn btn-remove" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="paging-buttons">
                    <asp:Button ID="btnPreviousCity" runat="server" Text="Previous" CssClass="btn-paging" OnClick="btnPreviousCity_Click" />
                    <asp:Button ID="btnNextCity" runat="server" Text="Next" CssClass="btn-paging" OnClick="btnNextCity_Click" />
                </div>
            </asp:Panel>
            <!-- Edit Districts Table -->
            <asp:Panel ID="pnlEditDistricts" runat="server" Visible="false">
                <div class="inline-controls">
                    <asp:TextBox ID="txtEditDistrictName" runat="server" CssClass="form-control search-box" Placeholder="District Name"></asp:TextBox>
                    <asp:Button ID="btnSaveDistrict" runat="server" Text="Save District" CssClass="btn btn-add" OnClick="btnSaveDistrict_Click" />
                    <asp:Button ID="btnCancelDistrict" runat="server" Text="Cancel" CssClass="btn btn-add" OnClick="btnCancelDistrict_Click" />
                </div>
                <asp:GridView ID="gvEditDistricts" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvEditDistricts_PageIndexChanging" OnRowCommand="gvEditDistricts_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="District_Id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="actions">
                                    <asp:Button ID="btnEditDistrict" runat="server" CommandName="EditDistrict" Text="Edit" CommandArgument='<%# Eval("District_Id") %>' CssClass="btn btn-edit" />
                                    <asp:Button ID="btnRemoveDistrict" runat="server" CommandName="RemoveDistrict" Text="Remove" CommandArgument='<%# Eval("District_Id") %>' CssClass="btn btn-remove" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="paging-buttons">
                    <asp:Button ID="btnPreviousDistrict" runat="server" Text="Previous" CssClass="btn-paging" OnClick="btnPreviousDistrict_Click" />
                    <asp:Button ID="btnNextDistrict" runat="server" Text="Next" CssClass="btn-paging" OnClick="btnNextDistrict_Click" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
