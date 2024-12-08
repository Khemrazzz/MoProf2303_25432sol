<%@ Page Title="" Language="C#" MasterPageFile="~/advertiserMasterPage.Master" AutoEventWireup="true" CodeBehind="adsEditProfilePage.aspx.cs" Inherits="MoProf2303_25432.adsEditProfilePage" %>

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
            <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">ADVERTISER</h5>
            <h1>Edit Profile</h1>
        </div>
        <div class="edit-profile-container">

    
        <asp:Label runat="server" CssClass="form-label">Company name</asp:Label>
        <asp:TextBox runat="server" ID="txtcname" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvcname" runat="server" ValidationGroup="vgads" ControlToValidate="txtcname" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="RequiredFieldValidator">Company name is required </asp:RequiredFieldValidator>
  

        <asp:Label runat="server"
            CssClass="form-label">Your Website URL</asp:Label>

        <asp:TextBox runat="server" ID="txturl"
            CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvurl" runat="server" ValidationGroup="vgads" ControlToValidate="txturl" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Url is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regurl" ControlToValidate="txturl" ValidationGroup="vgads" Display="Dynamic" ForeColor="Red" ValidationExpression="^(https?://)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(/.*)?$" runat="server" ErrorMessage="Invalid url"></asp:RegularExpressionValidator>

   
        <asp:Label runat="server" for="fuppicture5" CssClass="form-label">Upload profile picture</asp:Label>
        <asp:FileUpload ID="fuppicture5" runat="server" name="profilePicture" CssClass="form-control" />
         <asp:Image ID="imgProfilePicture" runat="server" CssClass="form-control"  Width="150px" Height="150px" />
            <br />
        <asp:Label runat="server" for="txtemail" CssClass="form-label">Email</asp:Label>
        <asp:TextBox runat="server" ID="txtemail" name="email" CssClass="form-control" TextMode="Email" />
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtemail" SetFocusOnError="true" ErrorMessage="Email is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regemail" ControlToValidate="txtemail" ValidationGroup="vgads" Display="Dynamic" ForeColor="Red" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$" runat="server" ErrorMessage="Invalid email"></asp:RegularExpressionValidator>
    
        <asp:Label runat="server" for="txtmob" CssClass="form-label">Mobile number</asp:Label>
        <asp:TextBox runat="server" ID="txtmob" name="mobileNumber" CssClass="form-control" onkeydown="return validateInput(event)" />
        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtmob" SetFocusOnError="true" ErrorMessage="Mobile number is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cvmob" runat="server" ForeColor="red" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtmob" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Mobile number is invalid"></asp:CompareValidator>
        <asp:RegularExpressionValidator ID="regmob" ControlToValidate="txtmob" ValidationGroup="vgads" Display="Dynamic" ValidationExpression="\d{8}" ForeColor="Red" runat="server" ErrorMessage="Mobile number must have 8 digits"></asp:RegularExpressionValidator>
    
        
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            
                <asp:Label runat="server" CssClass="form-label">District</asp:Label>
                <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                    <asp:ListItem Text="Select district" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvdistrict" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="ddldistrict" InitialValue="-1" SetFocusOnError="true" ErrorMessage="District is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
           
                <asp:Label runat="server" CssClass="form-label">Village/Town</asp:Label>
                <asp:DropDownList ID="ddlvt" runat="server">
                    <asp:ListItem Text="Select village/town" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvvt" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="ddlvt" InitialValue="-1" SetFocusOnError="true" ErrorMessage="Village/Town is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
            

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddldistrict" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
   
        <asp:Label runat="server" CssClass="form-label">Street address</asp:Label>
        <asp:TextBox runat="server" ID="txtstreetaddress" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvstreetaddress" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtstreetaddress" InitialValue="" SetFocusOnError="true" ErrorMessage="Street address is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

             <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgads" CssClass="btn" OnClick="btnSave_Click" />
 <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
  </div>
    </main>

</asp:Content>
