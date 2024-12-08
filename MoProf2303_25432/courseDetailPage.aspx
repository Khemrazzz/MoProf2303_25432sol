<%@ Page Title="" Language="C#" MasterPageFile="~/userMaster.Master" AutoEventWireup="true" CodeBehind="courseDetailPage.aspx.cs" Inherits="MoProf2303_25432.courseDetailPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

   <!-- Header Start -->
    <div class="jumbotron jumbotron-fluid page-header position-relative overlay-bottom" style="margin-bottom: 90px;">
        <div class="container text-center py-5">
            <h1 class="text-white display-1">Course Detail</h1>
            <div class="d-inline-flex text-white mb-5">
                <p class="m-0 text-uppercase"><a class="text-white" href="">Home</a></p>
                <i class="fa fa-angle-double-right pt-1 px-3"></i>
                <p class="m-0 text-uppercase">Course Detail</p>
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


    <!-- Detail Start -->
    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="row">
                <div class="col-lg-8">
                    <div class="mb-5">
                        <div class="section-title position-relative mb-5">
                            <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">Course Detail</h6>
                            <h1 id="courseTitle" runat="server" class="display-4"></h1>
                        </div>
                        <img id="courseImage" runat="server" class="img-fluid rounded w-100 mb-4" src="" alt="Image">
                        <h5 id="courseDescription" runat="server" class="mb-4"></h5>
                    </div>
                </div>

                <div class="col-lg-4 mt-5 mt-lg-0">
                    <div class="bg-primary mb-5 py-3">
                        <h3 class="text-white py-3 px-4 m-0">Course Features</h3>
                        <div class="d-flex justify-content-between border-bottom px-4">
                            <h6 class="text-white my-3">Instructor</h6>
                            <h6 id="courseInstructor" runat="server" class="text-white my-3"></h6>
                        </div>
                        <div class="d-flex justify-content-between border-bottom px-4">
                            <h6 class="text-white my-3">Description</h6>
                            <h6 id="H1" runat="server" class="text-white my-3"></h6>
                        </div>
                        <div class="d-flex justify-content-between border-bottom px-4">
                            <h6 class="text-white my-3">Lectures</h6>
                            <h6 id="courseLectures" runat="server" class="text-white my-3"></h6>
                        </div>
                        <div class="d-flex justify-content-between border-bottom px-4">
                            <h6 class="text-white my-3">Duration</h6>
                            <h6 id="courseDuration" runat="server" class="text-white my-3"></h6>
                        </div>
                        <div class="d-flex justify-content-between border-bottom px-4">
                            <h6 class="text-white my-3">Skill level</h6>
                            <h6 id="courseSkillLevel" runat="server" class="text-white my-3"></h6>
                        </div>
                        <div class="d-flex justify-content-between px-4">
                            <h6 class="text-white my-3">Language</h6>
                            <h6 id="courseLanguage" runat="server" class="text-white my-3"></h6>
                        </div>
                        <h5 id="coursePrice" runat="server" class="text-white py-3 px-4 m-0"></h5>
                        <div class="py-3 px-4">
                            <asp:Button ID="btnEnroll" runat="server" CssClass="btn btn-block btn-secondary py-3 px-5" Text="Enroll Now" OnClientClick="return confirmEnrollment();" OnClick="btnEnroll_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Detail End -->

    <script type="text/javascript">
        function confirmEnrollment() {
            return confirm("Are you sure you want to enroll in this course?");
        }
    </script>
</asp:Content>