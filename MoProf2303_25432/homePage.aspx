﻿<%@ Page Title="" Language="C#" MasterPageFile="~/userMaster.Master" AutoEventWireup="true" CodeBehind="homePage.aspx.cs" Inherits="MoProf2303_25432.homePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .team-item {
            position: relative;
            overflow: hidden;
            border-radius: 12px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            margin: 15px;
        }

        .team-item img {
            width: 100%;
            height: 300px;
            object-fit: cover;
        }

        .team-item .bg-light {
            padding: 20px;
            text-align: center;
        }

        .team-item h5 {
            font-size: 1.25rem;
            font-weight: 600;
            margin-bottom: 10px;
        }

        .team-item p {
            font-size: 1rem;
            color: #666;
            margin-bottom: 10px;
        }

        .team-item .d-flex {
            justify-content: center;
        }

        .team-item .d-flex a {
            color: #007bff;
            font-size: 1.25rem;
            margin: 0 5px;
            transition: color 0.3s;
        }

        .team-item .d-flex a:hover {
            color: #0056b3;
        }

        .review-item {
            border: 1px solid #ddd;
            padding: 10px;
            margin-bottom: 10px;
            border-radius: 5px;
            background-color: #f9f9f9;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <!-- Header Start -->
    <div class="jumbotron jumbotron-fluid position-relative overlay-bottom" style="margin-bottom: 90px;">
        <div class="container text-center my-5 py-5">
            <h1 class="text-white mt-4 mb-4">Learn From Home</h1>
            <h1 class="text-white display-1 mb-5">Education Courses</h1>
            <div class="mx-auto mb-5" style="width: 100%; max-width: 600px;">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <button class="btn btn-outline-light bg-white text-body px-4 dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            Courses
                        </button>
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

    <!-- About Start -->
    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="row">
                <div class="col-lg-5 mb-5 mb-lg-0" style="min-height: 500px;">
                    <div class="position-relative h-100">
                        <img class="position-absolute w-100 h-100" src="img/about.jpg" style="object-fit: cover;">
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="section-title position-relative mb-4">
                        <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">About Us</h6>
                        <h1 class="display-4">First Choice For Online Education Anywhere</h1>
                    </div>
                    <p>Tempor erat elitr at rebum at at clita aliquyam consetetur. Diam dolor diam ipsum et, tempor voluptua sit consetetur sit. Aliquyam diam amet diam et eos sadipscing labore. Clita erat ipsum et lorem et sit, sed stet no labore lorem sit. Sanctus clita duo justo et tempor consetetur takimata eirmod, dolores takimata consetetur invidunt magna dolores aliquyam dolores dolore. Amet erat amet et magna</p>
                    <div class="row pt-3 mx-0">
                        <div class="col-3 px-0">
                            <div class="bg-success text-center p-4">
                                <h1 class="text-white" data-toggle="counter-up">123</h1>
                                <h6 class="text-uppercase text-white">Available<span class="d-block">Subjects</span></h6>
                            </div>
                        </div>
                        <div class="col-3 px-0">
                            <div class="bg-primary text-center p-4">
                                <h1 class="text-white" data-toggle="counter-up">1234</h1>
                                <h6 class="text-uppercase text-white">Online<span class="d-block">Courses</span></h6>
                            </div>
                        </div>
                        <div class="col-3 px-0">
                            <div class="bg-secondary text-center p-4">
                                <h1 class="text-white" data-toggle="counter-up">123</h1>
                                <h6 class="text-uppercase text-white">Skilled<span class="d-block">Instructors</span></h6>
                            </div>
                        </div>
                        <div class="col-3 px-0">
                            <div class="bg-warning text-center p-4">
                                <h1 class="text-white" data-toggle="counter-up">1234</h1>
                                <h6 class="text-uppercase text-white">Happy<span class="d-block">Students</span></h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- About End -->


    <!-- Feature Start -->
    <div class="container-fluid bg-image" style="margin: 90px 0;">
        <div class="container">
            <div class="row">
                <div class="col-lg-7 my-5 pt-5 pb-lg-5">
                    <div class="section-title position-relative mb-4">
                        <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">Why Choose Us?</h6>
                        <h1 class="display-4">Why You Should Start Learning with Us?</h1>
                    </div>
                    <p class="mb-4 pb-2">Aliquyam accusam clita nonumy ipsum sit sea clita ipsum clita, ipsum dolores amet voluptua duo dolores et sit ipsum rebum, sadipscing et erat eirmod diam kasd labore clita est. Diam sanctus gubergren sit rebum clita amet.</p>
                    <div class="d-flex mb-3">
                        <div class="btn-icon bg-primary mr-4">
                            <i class="fa fa-2x fa-graduation-cap text-white"></i>
                        </div>
                        <div class="mt-n1">
                            <h4>Skilled Instructors</h4>
                            <p>Labore rebum duo est Sit dolore eos sit tempor eos stet, vero vero clita magna kasd no nonumy et eos dolor magna ipsum.</p>
                        </div>
                    </div>
                    <div class="d-flex mb-3">
                        <div class="btn-icon bg-secondary mr-4">
                            <i class="fa fa-2x fa-certificate text-white"></i>
                        </div>
                        <div class="mt-n1">
                            <h4>International Certificate</h4>
                            <p>Labore rebum duo est Sit dolore eos sit tempor eos stet, vero vero clita magna kasd no nonumy et eos dolor magna ipsum.</p>
                        </div>
                    </div>
                    <div class="d-flex">
                        <div class="btn-icon bg-warning mr-4">
                            <i class="fa fa-2x fa-book-reader text-white"></i>
                        </div>
                        <div class="mt-n1">
                            <h4>Online Classes</h4>
                            <p class="m-0">Labore rebum duo est Sit dolore eos sit tempor eos stet, vero vero clita magna kasd no nonumy et eos dolor magna ipsum.</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5" style="min-height: 500px;">
                    <div class="position-relative h-100">
                        <img class="position-absolute w-100 h-100" src="img/feature.jpg" style="object-fit: cover;">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Feature Start -->


    <!-- Courses Start -->
    <div class="container-fluid px-0 py-5">
        <div class="row mx-0 justify-content-center pt-5">
            <div class="col-lg-6">
                <div class="section-title text-center position-relative mb-4">
                    <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">Our Courses</h6>
                    <h1 class="display-4">Checkout New Releases Of Our Courses</h1>
                </div>
            </div>
        </div>
        <div class="owl-carousel courses-carousel">
            <asp:Repeater ID="rptCourses" runat="server">
                <ItemTemplate>
                    <div class="courses-item position-relative">
                        <img class="img-fluid" src='<%# ResolveUrl(Eval("CoursePicture").ToString()) %>' alt='<%# Eval("Subject_Name") %>' onerror="this.onerror=null;this.src='/path/to/default-image.jpg';">
                        <div class="courses-text">
                            <h4 class="text-center text-white px-3"><%# Eval("Subject_Name") %></h4>
                            <div class="border-top w-100 mt-3">
                                <div class="d-flex justify-content-between p-4">
                                    <span class="text-white"><i class="fa fa-user mr-2"></i><%# Eval("Tutor_Name") %></span>
                                </div>
                            </div>
                            <div class="w-100 bg-white text-center p-4">
                                <a class="btn btn-primary" href="courseDetailPage.aspx?CourseID=<%# Eval("Course_Id") %>">Course Detail</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <!-- Courses End -->

    <!-- Testimonial Start -->
    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="row align-items-center">
                <div class="col-lg-5 mb-5 mb-lg-0">
                    <div class="section-title position-relative mb-4">
                        <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">Testimonial</h6>
                        <h1 class="display-4">What Say Our Students</h1>
                    </div>
                    <p class="m-0">Dolor est dolores et nonumy sit labore dolores est sed rebum amet, justo duo ipsum sanctus dolore magna rebum sit et. Diam lorem ea sea at. Nonumy et at at sed justo est nonumy tempor. Vero sea ea eirmod, elitr ea amet diam ipsum at amet. Erat sed stet eos ipsum diam</p>
                </div>
                <div class="col-lg-7">
                    <div class="owl-carousel testimonial-carousel">
                        <asp:Repeater ID="rptTestimonials" runat="server">
                            <ItemTemplate>
                                <div class="bg-light p-5">
                                    <i class="fa fa-3x fa-quote-left text-primary mb-4"></i>
                                    <p><%# Eval("Message") %></p>
                                    <div class="d-flex flex-shrink-0 align-items-center mt-4">
                                        <img class="img-fluid mr-4" src='<%# ResolveUrl(Eval("ProfilePicture").ToString()) %>' alt='<%# Eval("FirstName") + " " + Eval("LastName") %>'>
                                    </div>
                                    <h5 class="m-0"><%# Eval("FirstName") %> <%# Eval("LastName") %></h5>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Testimonial End -->

    <!-- Team Start -->
    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="section-title text-center position-relative mb-5">
                <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">Instructors</h6>
                <h1 class="display-4">Meet Our Instructors</h1>
            </div>
            <div class="owl-carousel team-carousel position-relative" style="padding: 0 30px;">
                <asp:Repeater ID="rptInstructors" runat="server">
                    <ItemTemplate>
                        <div class="team-item">
                            <img class="img-fluid" src='<%# ResolveUrl(Eval("ProfilePicture").ToString()) %>' alt='<%# Eval("FirstName") + " " + Eval("LastName") %>' style="height: 320px; object-fit: cover;">
                            <div class="bg-light text-center p-4">
                                <h5><%# Eval("FirstName") + " " + Eval("LastName") %></h5>
                                <p><%# Eval("Status") %></p>
                                <p>Rating: <%# Eval("AverageRating") %>/5 (from <%# Eval("TotalReviews") %> reviews)</p>
                                <div class="d-flex justify-content-center">
                                    <a class="mx-1 p-1" href="#"><i class="fab fa-twitter"></i></a>
                                    <a class="mx-1 p-1" href="#"><i class="fab fa-facebook-f"></i></a>
                                    <a class="mx-1 p-1" href="#"><i class="fab fa-linkedin-in"></i></a>
                                    <a class="mx-1 p-1" href="#"><i class="fab fa-instagram"></i></a>
                                    <a class="mx-1 p-1" href="#"><i class="fab fa-youtube"></i></a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <!-- Team End -->

    <!-- Reviews Start -->
 <div class="container-fluid py-5">
    <div class="container py-5">
        <div class="row align-items-center">
            <div class="col-lg-5 mb-5 mb-lg-0">
             <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">Tutor Reviews</h6>
             <h1 class="display-4">What Students Say About Our Tutors</h1>
         </div>
<div class="col-lg-7">
                <div class="owl-carousel testimonial-carousel">
                    
         <asp:Repeater ID="rptReviews" runat="server">
             <ItemTemplate>
                 <div class="review-item">
                     <p><b>Student:</b> <%# Eval("StudentName") %></p>
                     <p><b>Tutor:</b> <%# Eval("TutorName") %></p>
                     <p><b>Rating:</b> <%# Eval("Rating") %>/5</p>
                     <p><b>Comments:</b> <%# Eval("Comments") %></p>
                     <p><b>Date:</b> <%# Eval("RatingDate", "{0:yyyy-MM-dd}") %></p>
                 </div>
             </ItemTemplate>
         </asp:Repeater>
</div>
            </div>

     </div>
 </div>
</div>
 <!-- Reviews End -->


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
                            <asp:RegularExpressionValidator ID="revemail" ControlToValidate="txtemail" Display="Dynamic" ForeColor="Red" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$" runat="server" ErrorMessage="Invalid email"></asp:RegularExpressionValidator>
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
