<%@ Page Title="" Language="C#" MasterPageFile="~/userMaster.Master" AutoEventWireup="true" CodeBehind="adminLogInPage.aspx.cs" Inherits="MoProf2303_25432.adminLogInPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="section-title text-center">
        <h3>Welcome Admin</h3>
        <p>"Proceed to the cave, Chief!"</p>
    </div>

    <hr class="invis">
    <div class="row">
        <!-- Form on the left side -->
        <div class="col-md-7">
            <div class="container py-5">
                <div class="text-center mb-5">
                    <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">Log In</h5>
                    <h1>A new day, a new task :)</h1>
                </div>
                <div class="row justify-content-center">
                    <div class="col-lg-8">

                        <fieldset>
                            <legend>login details</legend>
                            <div class="mb-3">
                                <asp:Label runat="server" CssClass="form-label">username</asp:Label>
                                <asp:TextBox runat="server" ID="txtuname" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvuname" runat="server" ControlToValidate="txtuname" ErrorMessage="Username is required" ForeColor="red" CssClass="help-block text-danger"></asp:RequiredFieldValidator>

                            </div>
                            <div class="mb-3">
                                <asp:Label runat="server" CssClass="form-label">password</asp:Label>
                                <asp:TextBox runat="server" ID="txtpass" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtpass" ErrorMessage="Password is required" ForeColor="red" CssClass="help-block text-danger"></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-check">
                                <asp:CheckBox runat="server" ID="chkRememberMe" Text="Remember Me" CssClass="form-check-input" />
                            </div>
                            <br />
                        </fieldset>
                        <div class="text-center">
                            <asp:Label ID="lblmsg" runat="server" ForeColor="red" CssClass="help-block text-danger" Text=""></asp:Label><br />
                             <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-primary py-3 px-5" Text="Sign In"  OnClick="btnLogin_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Image on the right side -->
        <div class="col-md-4">
            <div class="d-none d-md-block p-4">
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Image ID="Image2" ImageUrl="~/img/courses-1.jpg" Width="100%" Height="100%" runat="server" CssClass="img-fluid" />
            </div>
        </div>
    </div>
</asp:Content>