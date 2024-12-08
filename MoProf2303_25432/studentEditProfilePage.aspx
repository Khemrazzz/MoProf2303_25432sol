<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMasterPage.Master" AutoEventWireup="true" CodeBehind="studentEditProfilePage.aspx.cs" Inherits="MoProf2303_25432.studentEditProfilePage" %>
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
        .edit-profile-container textarea {
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
            <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">STUDENT</h5>
            <h1>Edit Profile</h1>
        </div>
       
        <div class="edit-profile-container">
            <asp:Label runat="server" CssClass="form-label">First name</asp:Label>
            <asp:TextBox runat="server" ID="txtfname" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvfname" runat="server" ValidationGroup="vgstudent" ControlToValidate="txtfname" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="First name is required ">First name is required</asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label">Middle name</asp:Label>
            <asp:TextBox runat="server" ID="txtmname" CssClass="form-control" />

            <asp:Label runat="server" CssClass="form-label">Last name</asp:Label>
            <asp:TextBox runat="server" ID="txtlname" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvlname" runat="server" ValidationGroup="vgstudent" ControlToValidate="txtlname" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="Last name is required">Last name is required</asp:RequiredFieldValidator>

            <asp:Label runat="server" for="ddlgender" CssClass="form-label">Gender</asp:Label>
            <br />
            <asp:DropDownList ID="ddlgender" runat="server" name="gender" CssClass="form-control">
                <asp:ListItem Text="Select gender" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvgender" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="ddlgender" InitialValue="" SetFocusOnError="true" ErrorMessage="Gender is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label">Date of birth</asp:Label>
            <asp:TextBox runat="server" ID="txtdob" name="dob" TextMode="Date" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvdob" runat="server" ValidationGroup="vgstudent" ControlToValidate="txtdob" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date of birth is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rvdob" runat="server" ValidationGroup="vgstudent" ControlToValidate="txtdob" Display="Dynamic" Text="Invalid Age" ForeColor="Red" ErrorMessage="Invalid date of birth"></asp:RangeValidator>

            <asp:Label runat="server" for="fuppicture" CssClass="form-label">Upload profile Picture</asp:Label>
            <asp:FileUpload ID="fuppicture" runat="server" name="profilePicture" CssClass="form-control" />
            <asp:Image ID="imgProfilePicture" runat="server" CssClass="form-control"  Width="150px" Height="150px" />

            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                <ContentTemplate>
                    <asp:Label runat="server" CssClass="form-label">Current status</asp:Label>
                    <asp:DropDownList ID="ddlcsts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcsts_SelectedIndexChanged">
                        <asp:ListItem Text="Choose status" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvcsts" runat="server" ValidationGroup="vgstudent" ControlToValidate="ddlcsts" InitialValue="-1" Display="Dynamic" SetFocusOnError="true" Text="Please select your current status" ForeColor="Red"></asp:RequiredFieldValidator>

                    <asp:Label runat="server" CssClass="form-label">Grade</asp:Label>
                    <asp:DropDownList ID="ddlgrade" runat="server">
                        <asp:ListItem Text="Choose grade" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvgrade" runat="server" ValidationGroup="vgstudent" ControlToValidate="ddlgrade" InitialValue="-1" Display="Dynamic" SetFocusOnError="true" Text="Please select your grade" ForeColor="Red"></asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlcsts" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:Label runat="server" CssClass="form-label">Bio</asp:Label>
            <asp:TextBox runat="server" ID="txtbio" TextMode="MultiLine" CssClass="form-control" />

            <asp:Label runat="server" for="txtemail" CssClass="form-label">Email</asp:Label>
            <asp:TextBox runat="server" ID="txtemail" name="email" CssClass="form-control" TextMode="Email" />
            <asp:RequiredFieldValidator ID="rfvEmail" ValidationGroup="vgstudent" Display="Dynamic" runat="server" ControlToValidate="txtemail" SetFocusOnError="true" ErrorMessage="Email is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regemail" ControlToValidate="txtemail" ValidationGroup="vgstudent" Display="Dynamic" ForeColor="Red" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$" runat="server" ErrorMessage="Invalid email"></asp:RegularExpressionValidator>

            <asp:Label runat="server" for="txtmob" CssClass="form-label">Mobile number</asp:Label>
            <asp:TextBox runat="server" ID="txtmob" name="mobileNumber" CssClass="form-control" onkeydown="return validateInput(event)" />
            <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="txtmob" SetFocusOnError="true" ErrorMessage="Mobile number is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvmob" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ForeColor="red" ControlToValidate="txtmob" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Mobile number is invalid"></asp:CompareValidator>
            <asp:RegularExpressionValidator ID="regmob" ControlToValidate="txtmob" ValidationGroup="vgstudent" Display="Dynamic" ValidationExpression="\d{8}" ForeColor="Red" runat="server" ErrorMessage="Mobile number must have 8 digits"></asp:RegularExpressionValidator>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" CssClass="form-label">District</asp:Label>
                    <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                        <asp:ListItem Text="Select district" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvdistrict" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="ddldistrict" InitialValue="-1" SetFocusOnError="true" ErrorMessage="District is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

                    <asp:Label runat="server" CssClass="form-label">Village/Town</asp:Label>
                    <asp:DropDownList ID="ddlvt" runat="server">
                        <asp:ListItem Text="Select village/town" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvvt" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="ddlvt" InitialValue="-1" SetFocusOnError="true" ErrorMessage="Village/Town is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddldistrict" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:Label runat="server" CssClass="form-label">Street Address</asp:Label>
            <asp:TextBox runat="server" ID="txtstreetaddress" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvstreetaddress" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="txtstreetaddress" InitialValue="" SetFocusOnError="true" ErrorMessage="Street address is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

            <asp:Button runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click" ValidationGroup="vgstudent" />
            <asp:Label ID="lblMessage" runat="server" CssClass="form-label"></asp:Label>
        </div>
    </main>
</asp:Content>
