<%@ Page Title="" Language="C#" MasterPageFile="~/stDashboard.Master" AutoEventWireup="true" CodeBehind="tutorEditCoursePage.aspx.cs" Inherits="MoProf2303_25432.tutorEditCoursePage" %>

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
            <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">TUTOR</h5>
            <h1>Edit Course</h1>
        </div>
        <div class="edit-profile-container">
            <asp:Label runat="server" for="fuppicture4" CssClass="form-label" Text="Upload course picture"></asp:Label>
            <asp:FileUpload ID="fuppicture4" runat="server" name="CoursePicture" CssClass="form-control" />
            <asp:HiddenField ID="fuppicture3" runat="server" />
            <asp:Image ID="imgCoursePicture" runat="server" Width="150px" Height="150px" />

            <br />

            <asp:Label runat="server" CssClass="form-label" Text="Course Name"></asp:Label>
            <asp:TextBox runat="server" ID="txtcname" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvcname" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtcname" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="Course name is required"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Category"></asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select Category" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ValidationGroup="vgtutor" ControlToValidate="ddlCategory" InitialValue="" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Category is required" ForeColor="red"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Description"></asp:Label>
            <asp:TextBox runat="server" ID="txtbio2" TextMode="MultiLine" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvbio2" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtbio2" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="Description is required"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Fees"></asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtfee" />
            <asp:RequiredFieldValidator ID="rfvfee" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtfee" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="Fee is required"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Date"></asp:Label>
            <asp:TextBox runat="server" ID="txtdate" TextMode="Date" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvdate" ValidationGroup="vgtutor" runat="server" ControlToValidate="txtdate" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rvdob2" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtdate" Display="Dynamic" Text="Invalid date" ForeColor="Red" ErrorMessage="Invalid date"></asp:RangeValidator>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" CssClass="form-label" Text="Day"></asp:Label>
                    <asp:DropDownList ID="ddlday" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlday_SelectedIndexChanged2">
                        <asp:ListItem Text="Select day" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvday" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlday" InitialValue="-1" SetFocusOnError="true" ErrorMessage="Day is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label runat="server" CssClass="form-label" Text="Time"></asp:Label>
                    <asp:DropDownList ID="ddltime" runat="server">
                        <asp:ListItem Text="Select time slot" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvtimeslot" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddltime" InitialValue="-1" SetFocusOnError="true" ErrorMessage="Time slot is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlday" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:Label runat="server" CssClass="form-label" Text="Mode"></asp:Label>
            <asp:DropDownList ID="ddlmode" runat="server">
                <asp:ListItem Text="Select mode" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvmode" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlmode" InitialValue="" SetFocusOnError="true" ErrorMessage="Mode is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" CssClass="form-label" Text="District"></asp:Label>
                    <asp:DropDownList ID="ddldistrict2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict2_SelectedIndexChanged">
                        <asp:ListItem Text="Select district" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvdistrict2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddldistrict2" InitialValue="-1" SetFocusOnError="true" ErrorMessage="District is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label runat="server" CssClass="form-label" Text="Village/Town"></asp:Label>
                    <asp:DropDownList ID="ddlvt2" runat="server">
                        <asp:ListItem Text="Select village/town" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvvt2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlvt2" InitialValue="-1" SetFocusOnError="true" ErrorMessage="Village/Town is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddldistrict2" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:Label runat="server" CssClass="form-label" Text="Street Address"></asp:Label>
            <asp:TextBox runat="server" ID="txtstreetaddress2" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvstreetaddress2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtstreetaddress2" InitialValue="" SetFocusOnError="true" ErrorMessage="Street address is mandatory" ForeColor="red"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-label" Text="Seats"></asp:Label>
            <asp:DropDownList ID="ddlseat" runat="server">
                <asp:ListItem Text="Select Seats" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvseat" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlseat" InitialValue="" SetFocusOnError="true" ErrorMessage="Seats are mandatory" ForeColor="red"></asp:RequiredFieldValidator>

            <div class="button">
                <asp:Button ID="SaveButton" runat="server" ValidationGroup="vgtutor" Text="Save" CssClass="btn" OnClick="btnSave_Click" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
            </div>
        </div>
    </main>
</asp:Content>
