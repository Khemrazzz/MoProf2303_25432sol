<%@ Page Title="" Language="C#" MasterPageFile="~/userMaster.Master" AutoEventWireup="true" CodeBehind="adsLogInPage.aspx.cs" Inherits="MoProf2303_25432.adsLogInPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="section-title text-center">
        <h3>Welcome</h3>
        <p>"Thank you for believing in us!"</p>
    </div>
    <hr class="invis">
    <div class="row">
        <!-- Form on the left side -->
        <div class="col-md-7">
            <div class="container py-5">
                <div class="text-center mb-5">
                    <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">Log In</h5>
                    <h1>A new day, a new record :)</h1>
                </div>
                <div class="row justify-content-center">
                    <div class="col-lg-8">
                        <fieldset>
                            <legend>Login Details</legend>
                            <div class="mb-3">
                                <asp:Label runat="server" CssClass="form-label">Username</asp:Label>
                                <asp:TextBox runat="server" ID="txtuname" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvuname" runat="server" ControlToValidate="txtuname" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="Username is required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <asp:Label runat="server" CssClass="form-label">Password</asp:Label>
                                <asp:TextBox runat="server" ID="txtpass" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtpass" ErrorMessage="Password is required" ForeColor="red" CssClass="help-block text-danger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-check">
                                <asp:CheckBox runat="server" ID="chkRememberMe" Text="Remember Me" CssClass="form-check-input" />
                            </div>
                            <br />
                        </fieldset>
                        <div class="text-center">
                            <asp:Button runat="server" ID="btnsignup1" CssClass="btn btn-primary py-3 px-5" Text="Log In" OnClick="sendMessageButton_Click" />
                        </div>
                        <!-- Login link -->
                        <div class="text-center mt-3">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="red" CssClass="help-block text-danger" Text=""></asp:Label><br />
                            <p>Don't have an account yet? <a href="adsRegistrationPage.aspx" class="text-primary">Sign Up</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Image on the right side -->
        <div class="col-md-4">
            <div class="d-none d-md-block p-4">
                <br /><br /><br /><br /><br /><br /><br />
                <asp:Image ID="Image2" ImageUrl="~/img/courses-1.jpg" Width="100%" Height="100%" runat="server" CssClass="img-fluid" />
            </div>
        </div>
    </div>
</asp:Content>