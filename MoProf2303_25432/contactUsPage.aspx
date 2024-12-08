<%@ Page Title="" Language="C#" MasterPageFile="~/userMaster.Master" AutoEventWireup="true" CodeBehind="contactUsPage.aspx.cs" Inherits="MoProf2303_25432.contactUsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <!-- Header Start -->
    <div class="jumbotron jumbotron-fluid page-header position-relative overlay-bottom" style="margin-bottom: 90px;">
        <div class="container text-center py-5">
            <h1 class="text-white display-1">Contact</h1>
            <div class="d-inline-flex text-white mb-5">
                <p class="m-0 text-uppercase"><a class="text-white" href="">Home</a></p>
                <i class="fa fa-angle-double-right pt-1 px-3"></i>
                <p class="m-0 text-uppercase">Contact</p>
            </div>
            <div class="mx-auto mb-5" style="width: 100%; max-width: 600px;">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <button class="btn btn-outline-light bg-white text-body px-4 dropdown-toggle" type="button" data-toggle="dropdown"
                            aria-haspopup="true" aria-expanded="false">
                            Courses</button>
                        <div class="dropdown-menu">
                            <a class="dropdown-item" href="#">Courses 1</a>
                            <a class="dropdown-item" href="#">Courses 2</a>
                            <a class="dropdown-item" href="#">Courses 3</a>
                        </div>
                    </div>
                    <input type="text" class="form-control border-light" style="padding: 30px 25px;" placeholder="Keyword">
                    <div class="input-group-append">
                        <button class="btn btn-secondary px-4 px-lg-5">Search</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Header End -->

    <!-- Contact Start -->
    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="row align-items-center">
                <div class="col-lg-5 mb-5 mb-lg-0">
                    <div class="bg-light d-flex flex-column justify-content-center px-5" style="height: 650px;">
                        <div class="d-flex align-items-center mb-5">
                            <div class="btn-icon bg-primary mr-4">
                                <i class="fa fa-2x fa-map-marker-alt text-white"></i>
                            </div>
                            <div class="mt-n1">
                                <h4>Our Location</h4>
                                <p class="m-0">Ave De La Concorde, La Tour Koenig, Pointe-aux-Sables, Mauritius</p>
                            </div>
                        </div>
                        <div class="d-flex align-items-center mb-5">
                            <div class="btn-icon bg-secondary mr-4">
                                <i class="fa fa-2x fa-phone-alt text-white"></i>
                            </div>
                            <div class="mt-n1">
                                <h4>Call Us</h4>
                                <p class="m-0">(+230) 207 5250</p>
                            </div>
                        </div>
                        <div class="d-flex align-items-center">
                            <div class="btn-icon bg-warning mr-4">
                                <i class="fa fa-2x fa-envelope text-white"></i>
                            </div>
                            <div class="mt-n1">
                                <h4>Email Us</h4>
                                <p class="m-0">info@MoProf.com</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="section-title position-relative mb-4">
                        <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">Need Help?</h6>
                        <h1 class="display-4">Send Us A Message</h1>
                    </div>
                    <div class="contact-form">
                        <div class="mb-3">
                            <asp:Label runat="server" CssClass="form-label">First name</asp:Label>
                            <asp:TextBox runat="server" ID="txtfname" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvfname" runat="server" ControlToValidate="txtfname" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="First name is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" CssClass="form-label">Last name</asp:Label>
                            <asp:TextBox runat="server" ID="txtlname" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvlname" runat="server" ControlToValidate="txtlname" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Last name is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-1">
                            <asp:Label runat="server" CssClass="form-label">Email</asp:Label>
                            <asp:TextBox runat="server" ID="txtemail" CssClass="form-control" />
                            <asp:RegularExpressionValidator ID="revemail" ControlToValidate="txtemail" ForeColor="Red" Display="Dynamic" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$" runat="server" ErrorMessage="Invalid email"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvemail" runat="server" ControlToValidate="txtemail" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" CssClass="form-label">Subject</asp:Label>
                            <asp:TextBox runat="server" ID="txtsubject" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvsubject" runat="server" ControlToValidate="txtsubject" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Subject is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" CssClass="form-label">Message</asp:Label>
                            <asp:TextBox runat="server" ID="txtmessage" TextMode="MultiLine" CssClass="form-control" />
                        </div>
                        <div>
                            <asp:Button ID="sendMessageButton" runat="server" CssClass="btn btn-primary py-3 px-5" Text="Send Message " OnClick="sendMessageButton_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Contact End -->
</asp:Content>
