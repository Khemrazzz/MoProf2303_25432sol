<%@ Page Title="" Language="C#" MasterPageFile="~/stDashboard.Master" AutoEventWireup="true" CodeBehind="tutorChangePassword.aspx.cs" Inherits="MoProf2303_25432.tutorChangePassword" %>
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

        function valPass_ClientValidate2(source, args) {
            if (args.Value.length <= 7 || args.Value.length >= 12) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        } 

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

      
        <main>
            <div class="text-center mb-5">
    <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">TUTOR</h5>
    <h1>Change Password</h1>
</div>
<div class="edit-profile-container">
    <div>
    <asp:Label runat="server" for="txtcurrentpass" CssClass="form-label">Current Password</asp:Label>
    <asp:TextBox runat="server" ID="txtcurrentpass" name="password" TextMode="Password" CssClass="form-control" />
    <asp:CustomValidator ID="CVcurrentpass" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtcurrentpass" ForeColor="Red" ClientValidationFunction="valPass_ClientValidate2" OnServerValidate="cusvpass_ServerValidate2" ErrorMessage="Password should be 7 to 12 characters"></asp:CustomValidator>

    <asp:RequiredFieldValidator ID="rfvccpass" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtcurrentpass" SetFocusOnError="true" ErrorMessage="Password is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
        </div>
    <div>
    <asp:Label runat="server" for="txtpass2" CssClass="form-label">Password</asp:Label>
    <asp:TextBox runat="server" ID="txtpass2" name="password" TextMode="Password" CssClass="form-control" />
    <asp:CustomValidator ID="cusvpass2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtpass2" ForeColor="Red" ClientValidationFunction="valPass_ClientValidate2" OnServerValidate="cusvpass_ServerValidate2" ErrorMessage="Password should be 7 to 12 characters"></asp:CustomValidator>

    <asp:RequiredFieldValidator ID="rfvpass2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtpass2" SetFocusOnError="true" ErrorMessage="Password is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
    </div>
    <div>
    <asp:Label runat="server" for="txtcpass2" CssClass="form-label">Confirm Password</asp:Label>
    <asp:TextBox runat="server" ID="txtcpass2" name="confirmPassword" TextMode="Password" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvcpass2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtcpass2" SetFocusOnError="true" ErrorMessage="Confirm Password is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="cvcpass2" runat="server" ForeColor="red" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtcpass2" ControlToCompare="txtpass2" Operator="Equal" Type="String" ErrorMessage="Passwords do not match"></asp:CompareValidator>
            </div>
    <div class="button">
    <asp:Button ID="SaveButton" runat="server" ValidationGroup="vgtutor" Text="Save" CssClass="btn" OnClick="SaveButton_Click" />
        <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
    
</div>
<a href="#"><p>Forget password?</p></a>
    </div>
        </main>

       

</asp:Content>
