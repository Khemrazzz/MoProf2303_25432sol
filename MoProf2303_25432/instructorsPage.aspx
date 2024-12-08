<%@ Page Title="" Language="C#" MasterPageFile="~/userMaster.Master" AutoEventWireup="true" CodeBehind="instructorsPage.aspx.cs" Inherits="MoProf2303_25432.instructorsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         function triggerPostBack() {
             __doPostBack('<%= txtSearch.UniqueID %>', '');
         }
     </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <!-- Header Start -->
    <div class="jumbotron jumbotron-fluid page-header position-relative overlay-bottom" style="margin-bottom: 90px;">
        <div class="container text-center py-5">
            <h1 class="text-white display-1">Instructors</h1>
            <div class="d-inline-flex text-white mb-5">
                <p class="m-0 text-uppercase"><a class="text-white" href="">Home</a></p>
                <i class="fa fa-angle-double-right pt-1 px-3"></i>
                <p class="m-0 text-uppercase">Instructors</p>
            </div>
           
            <div class="mx-auto mb-5" style="width: 100%; max-width: 600px;">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control border-light" 
                                 style="padding: 30px 25px;" placeholder="Search by Name" 
                                 AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" 
                                 onkeyup="triggerPostBack()"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <!-- Header End -->

    <!-- Team Start -->
    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="section-title text-center position-relative mb-5">
                <h6 class="d-inline-block position-relative text-secondary text-uppercase pb-2">Instructors</h6>
                <h1 class="display-4">Meet Our Instructors</h1>
            </div>
           <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
               <ContentTemplate>
                   <div class="row">
                       <asp:Repeater ID="rptInstructors" runat="server">
                           <ItemTemplate>
                               <div class="col-lg-3 col-md-4 col-sm-6 mb-4">
                                   <div class="team-item">
                                       <img class="img-fluid" src='<%# ResolveUrl(Eval("ProfilePicture").ToString()) %>' 
                                            alt='<%# Eval("FirstName") + " " + Eval("LastName") %>' style="height: 320px; object-fit: cover;">
                                       <div class="bg-light text-center p-4">
                                           <h5 runat="server" ID="test"><%# Eval("FirstName") + " " + Eval("LastName") %></h5>
                                           <p><%# Eval("Status") %></p>
                                           <p>Rating: <%# Eval("AverageRating").ToString() %>/5 (from <%# Eval("TotalReviews") %> reviews)</p>
                                           <div class="d-flex justify-content-center">
                                               <a class="mx-1 p-1" href="#"><i class="fab fa-twitter"></i></a>
                                               <a class="mx-1 p-1" href="#"><i class="fab fa-facebook-f"></i></a>
                                               <a class="mx-1 p-1" href="#"><i class="fab fa-linkedin-in"></i></a>
                                               <a class="mx-1 p-1" href="#"><i class="fab fa-instagram"></i></a>
                                               <a class="mx-1 p-1" href="#"><i class="fab fa-youtube"></i></a>
                                           </div>
                                       </div>
                                   </div>
                               </div>
                           </ItemTemplate>
                       </asp:Repeater>
                   </div>
               </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
               </Triggers>
           </asp:UpdatePanel>
        </div>
    </div>
    <!-- Team End -->

</asp:Content>