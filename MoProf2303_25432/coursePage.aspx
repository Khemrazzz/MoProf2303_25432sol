<%@ Page Title="" Language="C#" MasterPageFile="~/userMaster.Master" AutoEventWireup="true" CodeBehind="coursePage.aspx.cs" Inherits="MoProf2303_25432.coursePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <!-- Header Start -->
    <div class="jumbotron jumbotron-fluid page-header position-relative overlay-bottom" style="margin-bottom: 90px;">
        <div class="container text-center py-5">
            <h1 class="text-white display-1">Courses</h1>
            <div class="d-inline-flex text-white mb-5">
                <p class="m-0 text-uppercase"><a class="text-white" href="">Home</a></p>
                <i class="fa fa-angle-double-right pt-1 px-3"></i>
                <p class="m-0 text-uppercase">Courses</p>
            </div>
            <div class="mx-auto mb-5 search-container">
                <asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control border-light" style="padding: 30px 25px;" placeholder="Search by name"></asp:TextBox>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select border-light" />
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-select border-light" />
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-secondary px-4 px-lg-5" Text="Search" OnClick="SearchCourses" />
            </div>
        </div>
    </div>
    <!-- Header End -->

    <!-- Courses Start -->
    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="row mx-0 justify-content-center">
                <div class="col-lg-8">
                    <div class="section-title text-center position-relative mb-5">
                        <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">Our Courses</h6>
                        <h1 class="display-4">Checkout New Releases Of Our Courses</h1>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-6 pb-4">
                            <a class="courses-list-item position-relative d-block overflow-hidden mb-2" href="courseDetailPage.aspx?CourseID=<%# Eval("Course_Id") %>">
                                <img class="img-fluid" src='<%# ResolveUrl(Eval("CoursePicture").ToString()) %>' alt='<%# Eval("Subject_Name") %>' onerror="this.onerror=null;this.src='/path/to/default-image.jpg';">
                                <div class="courses-text">
                                    <h4 class="text-center text-white px-3"><%# Eval("Subject_Name") %></h4>
                                    <div class="border-top w-100 mt-3">
                                        <div class="d-flex justify-content-between p-4">
                                            <span class="text-white"><i class="fa fa-user mr-2"></i><%# Eval("Tutor_Name") %></span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <!-- Courses End -->
</asp:Content>