<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMasterPage.Master" AutoEventWireup="true" CodeBehind="studentTestimonials.aspx.cs" Inherits="MoProf2303_25432.studentTestimonials" %>
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
    <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">STUDENT</h5>
    <h1>Share your journey with us</h1>
</div>
<div class="edit-profile-container">

            <asp:Label runat="server" for="txtfName" CssClass="form-label">First Name</asp:Label>
        <asp:TextBox runat="server" ID="txtfName" name="firstName" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvfname" ValidationGroup="vgtesti" runat="server" ControlToValidate="txtfName" SetFocusOnError="true" ErrorMessage="First name is mandatory" ForeColor="red"></asp:RequiredFieldValidator>


    
        <asp:Label runat="server" for="Textlname" CssClass="form-label">Last Name</asp:Label>
        <asp:TextBox runat="server" ID="Textlname" name="lastName" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvlname" runat="server" ValidationGroup="vgtesti" ControlToValidate="Textlname" SetFocusOnError="true" ErrorMessage="Last name is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
   
        <asp:Label runat="server" for="txtemail" CssClass="form-label">Email</asp:Label>
        <asp:TextBox runat="server" ID="txtemail" name="email" CssClass="form-control" TextMode="Email" />
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ValidationGroup="vgtesti" ControlToValidate="txtemail" SetFocusOnError="true" ErrorMessage="Email is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regemail" ControlToValidate="txtemail" ValidationGroup="vgtesti" ForeColor="Red" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$" runat="server" ErrorMessage="Invalid email"></asp:RegularExpressionValidator>
 

        <asp:Label runat="server" CssClass="form-label">Message</asp:Label>
        <asp:TextBox runat="server" ID="txtmessage" AutoPostBack="true" TextMode="Multiline" CssClass="form-control" Height="89px" />
        <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ValidationGroup="vgtesti" ControlToValidate="txtmessage" ErrorMessage="Message is required" ForeColor="red" CssClass="help-block text-danger"></asp:RequiredFieldValidator>

<div class="text-center">
    <asp:Button runat="server" ID="submitionButton" ValidationGroup="vgtesti" CssClass="btn btn-primary py-3 px-5" Text="Submit Testimonial" OnClick="submitButton_Click" />
</div>
</div>
                </main>

</asp:Content>
