<%@ Page Title="" Language="C#" MasterPageFile="~/stDashboard.Master" AutoEventWireup="true" CodeBehind="tutorEditProfilePage.aspx.cs" Inherits="MoProf2303_25432.tutorEditProfilePage" %>

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
    <script>
        function validateInput(event) {
            return ['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight'].includes(event.code) ? true : !isNaN(Number(event.key)) && event.code !== 'Space';
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <main>
        <div class="text-center mb-5">
            <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">TUTOR</h5>
            <h1>Edit Profile</h1>
        </div>
        <div class="edit-profile-container">
            <asp:Label runat="server" CssClass="form-label" Text="Status"></asp:Label>
            <asp:DropDownList CssClass="form-control" ID="ddlstatus" runat="server">
                <asp:ListItem Text="Available" Value="Available"></asp:ListItem>
                <asp:ListItem Text="Unavailable" Value="Unavailable"></asp:ListItem>
            </asp:DropDownList>

            <asp:Label runat="server" CssClass="form-label" Text="First name"></asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtfname2" />
            <asp:RequiredFieldValidator ID="rfvfname2" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtfname2" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="First name is required"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Middle name"></asp:Label>
            <asp:TextBox runat="server" ID="txtmname2" CssClass="form-control" />

            <asp:Label runat="server" CssClass="form-label" Text="Last name"></asp:Label>
            <asp:TextBox runat="server" ID="txtlname2" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvlname2" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtlname2" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="Last name is required"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Bio"></asp:Label>
            <asp:TextBox runat="server" ID="txtbio2" TextMode="MultiLine" CssClass="form-control" />

            <asp:Label runat="server" CssClass="form-label" Text="Gender"></asp:Label>
            <asp:DropDownList ID="ddlgender2" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select gender" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvgender2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlgender2" InitialValue="" SetFocusOnError="true" ErrorMessage="Gender is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Date of birth"></asp:Label>
            <asp:TextBox runat="server" ID="txtdob2" TextMode="Date" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvdob2" ValidationGroup="vgtutor" runat="server" ControlToValidate="txtdob2" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date of birth is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rvdob2" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtdob2" Display="Dynamic" Text="Invalid Age" ForeColor="Red" ErrorMessage="Invalid date of birth"></asp:RangeValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Upload profile Picture"></asp:Label>
            <asp:FileUpload ID="fuppicture7" runat="server" CssClass="form-control" />
            <asp:HiddenField ID="fuppicture3" runat="server" />
            <asp:Image ID="imgProfilePicture" runat="server" Width="150px" Height="150px" />

            <br />

            <asp:Label runat="server" CssClass="form-label" Text="Email"></asp:Label>
            <asp:TextBox runat="server" ID="txtemail2" CssClass="form-control" TextMode="Email" />
            <asp:RequiredFieldValidator ID="rfvemail2" ValidationGroup="vgtutor" Display="Dynamic" runat="server" ControlToValidate="txtemail2" SetFocusOnError="true" ErrorMessage="Email is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regemail3" ControlToValidate="txtemail2" ValidationGroup="vgtutor" Display="Dynamic" ForeColor="Red" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$" runat="server" ErrorMessage="Invalid email"></asp:RegularExpressionValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Mobile number"></asp:Label>
            <asp:TextBox runat="server" ID="txtmob2" CssClass="form-control" onkeydown="return validateInput(event)" />
            <asp:RequiredFieldValidator ID="rfvmob2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtmob2" SetFocusOnError="true" ErrorMessage="Mobile number is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvmob2" runat="server" ForeColor="red" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtmob2" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Mobile number is invalid"></asp:CompareValidator>
            <asp:RegularExpressionValidator ID="regmob2" ControlToValidate="txtmob2" ValidationGroup="vgtutor" Display="Dynamic" ValidationExpression="\d{8}" ForeColor="Red" runat="server" ErrorMessage="Mobile number must have 8 digits"></asp:RegularExpressionValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Country"></asp:Label>
            <asp:DropDownList ID="ddlcountry2" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select Country" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvcountry2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlcountry2" InitialValue="" SetFocusOnError="true" ErrorMessage="Country is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" CssClass="form-label" Text="District"></asp:Label>
                    <asp:DropDownList ID="ddldistrict2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged2">
                        <asp:ListItem Text="Select district" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvdistrict2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddldistrict2" InitialValue="-1" SetFocusOnError="true" ErrorMessage="District is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

                    <asp:Label runat="server" CssClass="form-label" Text="Village/Town"></asp:Label>
                    <asp:DropDownList ID="ddlvt2" runat="server">
                        <asp:ListItem Text="Select village/town" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvvt2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlvt2" InitialValue="-1" SetFocusOnError="true" ErrorMessage="village/town is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddldistrict2" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:Label runat="server" CssClass="form-label" Text="Street Address"></asp:Label>
            <asp:TextBox runat="server" ID="txtstreetaddress2" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvstreetaddress2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtstreetaddress2" InitialValue="" SetFocusOnError="true" ErrorMessage="Street address is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgtutor" CssClass="btn" OnClick="btnSave_Click" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </main>
</asp:Content>
