<%@ Page Title="" Language="C#" MasterPageFile="~/stDashboard.Master" AutoEventWireup="true" CodeBehind="tutorStudentsPage.aspx.cs" Inherits="MoProf2303_25432.tutorStudentsPage" %>
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
    .card-container {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        gap: 1rem;
    }
    .card {
        border: 1px solid #ccc;
        border-radius: 8px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        width: 350px;
        padding: 2.5rem;
        background-color: #e7f3ff;
        text-align: center;
    }
    .card-title {
        font-size: 1.4rem;
        font-weight: bold;
        margin-bottom: 0.5rem;
    }
    .card-text {
        font-size: 1.1rem;
        margin-bottom: 0.5rem;
    }
    .btn {
        display: block;
        width: 100%;
        margin: 0.3rem 0;
        padding: 0.6rem;
        border: none;
        border-radius: 4px;
        color: #fff;
        cursor: pointer;
        font-size: 1.1rem;
        transition: background 0.3s;
    }
    .btn-view {
        background-color: #007bff;
    }
    .btn-view:hover {
        background-color: #0056b3;
    }
    .btn-accept {
        background-color: #28a745;
    }
    .btn-accept:hover {
        background-color: #218838;
    }
    .btn-reject {
        background-color: #dc3545;
    }
    .btn-reject:hover {
        background-color: #c82333;
    }
    .modal {
        display: none;
        position: fixed;
        z-index: 1;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        overflow: auto;
        background-color: rgb(0,0,0);
        background-color: rgba(0,0,0,0.4);
        padding-top: 60px;
    }
    .modal-content {
        background-color: #fefefe;
        margin: 5% auto;
        padding: 20px;
        border: 1px solid #888;
        width: 50%;
        max-width: 700px;
    }
    .close {
        color: #aaa;
        float: right;
        font-size: 28px;
        font-weight: bold;
    }
    .close:hover,
    .close:focus {
        color: black;
        text-decoration: none;
        cursor: pointer;
    }
    .header {
        display: flex;
        justify-content: center;
        text-align: center;
        width: 100%;
        margin-bottom: 2rem;
    }
    .header h1 {
        font-size: 2.5rem;
    }
    .header .subheader {
        font-size: 1.2rem;
        color: #555;
    }
</style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <main>
        <div class="header text-center mb-5">
            <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">TUTOR</h5>
            <h1>My Courses</h1>
        </div>
        <div class="card-container">
            <asp:Repeater ID="rptStudents" runat="server" OnItemCommand="rptStudents_ItemCommand">
                <ItemTemplate>
                    <div class="card">
                        <h5 class="card-title"><%# Eval("FirstName") %> <%# Eval("LastName") %></h5>
                        <p class="card-text"><%# Eval("Grade_Name") %></p>
                        <p class="card-text"><%# Eval("Email") %></p>
                        <asp:Button ID="btnViewCertificate" runat="server" Text="View Certificate" CommandName="ViewCertificate" CommandArgument='<%# ResolveUrl(Eval("CertificatePicture").ToString()) %>' CssClass="btn btn-view" />
                        <asp:Button ID="btnAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("Booking_Id") %>' CssClass="btn btn-accept" />
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("Booking_Id") %>' CssClass="btn btn-reject" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- The Modal -->
        <div id="myModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="document.getElementById('myModal').style.display='none'">&times;</span>
                <img id="resultImage" src="" alt="Result Image" style="width:100%">
            </div>
        </div>
    </main>
    <script>
        function showResultImage(imageUrl) {
            var modal = document.getElementById("myModal");
            var img = document.getElementById("resultImage");
            img.src = imageUrl;
            modal.style.display = "block";
        }
    </script>
</asp:Content>