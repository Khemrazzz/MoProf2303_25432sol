<%@ Page Title="" Language="C#" MasterPageFile="~/userMaster.Master" AutoEventWireup="true" CodeBehind="registrationPage.aspx.cs" Inherits="MoProf2303_25432.registrationPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .message-box {
            display: flex;
            justify-content: center;
        }

        .navbarrr-buttons {
            display: flex;
            gap: 10px; /* Adjusts the space between the buttons */
        }

        .nav-item {
            list-style: none;
        }
    </style>
    <script type="text/javascript">
        function validateCheckBox(sender, args) {
            var chkAgree = document.getElementById('<%= chkAgree.ClientID %>');
            var chkAgree2 = document.getElementById('<%= chkAgree2.ClientID %>');

            if (chkAgree.checked) {
                args.IsValid = true;
                return;
            } else {
                args.IsValid = chkAgree2.checked;
            }
        }

        function validateCheckBox2(sender, args) {
            var chkAgree2 = document.getElementById('<%= chkAgree2.ClientID %>');
            var chkAgree = document.getElementById('<%= chkAgree.ClientID %>');

            if (chkAgree2.checked) {
                args.IsValid = true;
                return;
            } else {
                args.IsValid = chkAgree.checked;
            }
        }

        function validateInput(event) {
            return ['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight'].includes(event.code) ? true : !isNaN(Number(event.key)) && event.code !== 'Space';
        }

        function valPass_ClientValidate(source, args) {
            if (args.Value.length <= 7 || args.Value.length >= 12) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        } 

        function valPass_ClientValidate2(source, args) {
            if (args.Value.length <= 7 || args.Value.length >= 12) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        } 

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    
    <div class="section-title text-center">
        <h3>Choose your title</h3>
        <p>Select your appropriate role to complete your registration!</p>
    </div>

    <div class="col-md-6 offset-md-3">
        <div class="message-box d-flex justify-content-center">
            <ul class="navbarrr-buttons nav nav-pills nav-stacked" id="myTabs">
                <li class="nav-item">
                    <a class="active btn btn-primary py-2 px-4" href="#tab1"  data-toggle="pill">Student</a>
                </li>
                <li class="nav-item">
                    <a class="btn btn-primary py-2 px-4" href="#tab2"  data-toggle="pill">Tutor</a>
                </li>
            </ul>
        </div>
    </div>

    <hr class="invis">

    <div class="row">
        <div class="col-md-12">
            <div class="tab-content">
                <div class="tab-pane active fade show" id="tab1">
                    <div class="row">
                        <!-- Form on the left side -->
                        <div class="col-md-7">
                            <div class="container py-5">
                                <div class="text-center mb-5">
                                    <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">Registration</h5>
                                    <h1>Sign up to start learning</h1>
                                </div>
                                <div class="row justify-content-center">
                                    <div class="col-lg-8">
                                        <fieldset>
                                            <legend>Personal Details</legend>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">First name</asp:Label>
                                                <asp:TextBox runat="server" ID="txtfname" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvfname" runat="server" ValidationGroup="vgstudent" ControlToValidate="txtfname" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="RequiredFieldValidator">First name is required </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Middle name</asp:Label>
                                                <asp:TextBox runat="server" ID="txtmname" CssClass="form-control" />
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Last name</asp:Label>
                                                <asp:TextBox runat="server" ID="txtlname" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvlname" runat="server" ValidationGroup="vgstudent" ControlToValidate="txtlname" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="RequiredFieldValidator">Last name is required </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="ddlgender" CssClass="form-label"> Gender</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlgender" runat="server" name="gender" CssClass="form-control">
                                                    <asp:ListItem Text="Select gender" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvgender" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="ddlgender" InitialValue="" SetFocusOnError="true" ErrorMessage="Gender is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="mb-3">
                                                <div class="form-check-inline">
                                                    <asp:RadioButton runat="server" GroupName="gender" ID="radMr" />
                                                    <asp:Label runat="server" CssClass="form-check-label">Mr</asp:Label>
                                                </div>
                                                <div class="form-check-inline">
                                                    <asp:RadioButton runat="server" GroupName="gender" ID="radMrs" />
                                                    <asp:Label runat="server" CssClass="form-check-label">Mrs</asp:Label>
                                                </div>
                                                <div class="form-check-inline">
                                                    <asp:RadioButton runat="server" GroupName="gender" ID="radMiss" />
                                                    <asp:Label runat="server" CssClass="form-check-label">Miss</asp:Label>
                                                </div>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Date of birth</asp:Label>
                                                <asp:TextBox runat="server" ID="txtdob" name="dob" TextMode="Date" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvdob" runat="server" ValidationGroup="vgstudent" ControlToValidate="txtdob" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date of birth is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="rvdob" runat="server" ValidationGroup="vgstudent" ControlToValidate="txtdob" Display="Dynamic" Text="Invalid Age" ForeColor="Red" ErrorMessage="Invalid date of birth"></asp:RangeValidator>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="fuppicture" CssClass="form-label">Upload profile Picture</asp:Label>
                                                <asp:FileUpload ID="fuppicture" runat="server" name="profilePicture" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvpicture" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="fuppicture" SetFocusOnError="true" ErrorMessage="Picture is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>Education details</legend>

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                                <ContentTemplate>

                                                    <div class="mb-3">
                                                        <asp:Label runat="server" CssClass="form-label">Current status</asp:Label>
                                                        <asp:DropDownList ID="ddlcsts" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlcsts_SelectedIndexChanged">
                                                            <asp:ListItem Text="Choose status" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvcsts" runat="server" ValidationGroup="vgstudent" ControlToValidate="ddlcsts" InitialValue="-1" Display="Dynamic" SetFocusOnError="true" Text="Please select your current status" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="mb-3">
                                                        <asp:Label runat="server" CssClass="form-label">Grade</asp:Label>
                                                        <asp:DropDownList ID="ddlgrade" runat="server">
                                                            <asp:ListItem Text="Choose grade" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvgrade" runat="server" ValidationGroup="vgstudent" ControlToValidate="ddlgrade" InitialValue="-1" Display="Dynamic" SetFocusOnError="true" Text="Please select your grade" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlcsts" EventName="SelectedIndexChanged"  />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Bio</asp:Label>
                                                <asp:TextBox runat="server" ID="txtbio" TextMode="MultiLine" CssClass="form-control" />
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="fuppicture2" CssClass="form-label">Upload your latest certificate</asp:Label>
                                                <asp:FileUpload ID="fuppicture2" runat="server" name="certicatePicture" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvpicture2" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="fuppicture2" SetFocusOnError="true" ErrorMessage="Certificate is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>contact details</legend>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtemail" CssClass="form-label">Email</asp:Label>
                                                <asp:TextBox runat="server" ID="txtemail" name="email" CssClass="form-control" TextMode="Email" />
                                                <asp:RequiredFieldValidator ID="rfvEmail" ValidationGroup="vgstudent" Display="Dynamic" runat="server" ControlToValidate="txtemail" SetFocusOnError="true" ErrorMessage="Email is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="regemail" ControlToValidate="txtemail" ValidationGroup="vgstudent" Display="Dynamic" ForeColor="Red" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$" runat="server" ErrorMessage="Invalid email"></asp:RegularExpressionValidator>
                                            </div>

                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtmob" CssClass="form-label">Mobile number</asp:Label>
                                                <asp:TextBox runat="server" ID="txtmob" name="mobileNumber" CssClass="form-control" onkeydown="return validateInput(event)" />
                                                <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="txtmob" SetFocusOnError="true" ErrorMessage="Mobile number is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvmob" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ForeColor="red" ControlToValidate="txtmob" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Mobile number is invalid"></asp:CompareValidator>
                                                <asp:RegularExpressionValidator ID="regmob" ControlToValidate="txtmob" ValidationGroup="vgstudent" Display="Dynamic" ValidationExpression="\d{8}" ForeColor="Red" runat="server" ErrorMessage="Mobile number must have 8 digits"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Country</asp:Label>
                                                <asp:DropDownList ID="ddlcountry" runat="server" name="country">
                                                    <asp:ListItem Text="Select Country" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvcountry" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="ddlcountry" InitialValue="" SetFocusOnError="true" ErrorMessage="Country is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="mb-3">
                                                        <asp:Label runat="server" CssClass="form-label">District</asp:Label>
                                                        <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                                            <asp:ListItem Text="Select district" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvdistrict" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="ddldistrict" InitialValue="-1" SetFocusOnError="true" ErrorMessage="District is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="mb-3">
                                                        <asp:Label runat="server" CssClass="form-label">Village/Town</asp:Label>
                                                        <asp:DropDownList ID="ddlvt" runat="server">
                                                            <asp:ListItem Text="Select village/town" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvvt" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="ddlvt" InitialValue="-1" SetFocusOnError="true" ErrorMessage="Village/Town is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddldistrict" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">street Address</asp:Label>
                                                <asp:TextBox runat="server" ID="txtstreetaddress" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvstreetaddress" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="txtstreetaddress" InitialValue="" SetFocusOnError="true" ErrorMessage="Street address is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>login details</legend>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtuname" CssClass="form-label">Username</asp:Label>
                                                <asp:TextBox runat="server" ID="txtuname" name="username" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="txtuname" SetFocusOnError="true" ErrorMessage="Username is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtpass" CssClass="form-label">Password</asp:Label>
                                                <asp:TextBox runat="server" ID="txtpass" name="password" TextMode="Password" CssClass="form-control" />
                                                <asp:CustomValidator ID="cusvpass" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="txtpass" ForeColor="Red" ClientValidationFunction="valPass_ClientValidate" OnServerValidate="cusvpass_ServerValidate" ErrorMessage="Password should be 7 to 12 characters"></asp:CustomValidator>

                                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="txtpass" SetFocusOnError="true" ErrorMessage="Password is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>



                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtcpass" CssClass="form-label">Confirm Password</asp:Label>
                                                <asp:TextBox runat="server" ID="txtcpass" name="confirmPassword" TextMode="Password" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvConfirmPassword" ValidationGroup="vgstudent" Display="Dynamic" runat="server" ControlToValidate="txtcpass" SetFocusOnError="true" ErrorMessage="Confirm Password is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvPassword" runat="server" ForeColor="red" ValidationGroup="vgstudent" Display="Dynamic" ControlToValidate="txtcpass" ControlToCompare="txtpass" Operator="Equal" Type="String" ErrorMessage="Passwords do not match"></asp:CompareValidator>

                                                <div>
                                                    <asp:CheckBox ID="chkAgree" runat="server" Text="Term & Conditions and Privacy Statement" />
                                                    <br />
                                                    <asp:CustomValidator ID="cvAgree" runat="server" ValidationGroup="vgstudent"
                                                        ClientValidationFunction="validateCheckBox"
                                                        ErrorMessage="Please agree to the terms and conditions."
                                                        ForeColor="Red"
                                                        Display="Dynamic">
                                                    </asp:CustomValidator>
                                                </div>
                                            </div>

                                        </fieldset>
                                        <br />
                                        <div class="text-center">
                                            <asp:Button ID="registerButton" runat="server" ValidationGroup="vgstudent" CssClass="btn btn-primary py-3 px-5" Text="Sign Up" OnClick="registerButton_Click" />

                                        </div>
                                        <!-- Login link -->
                                        <div class="text-center mt-3">
                                            <p>Already have an account? <a href="login.aspx" class="text-primary">Log In</a></p>
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
                </div>
                <!-- end pane -->

                <div class="tab-pane fade" id="tab2">
                    <div class="row">
                        <!-- Form on the left side -->
                        <div class="col-md-7">
                            <div class="container py-5">
                                <div class="text-center mb-5">
                                    <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">Registration</h5>
                                    <h1>Sign up to start teaching</h1>
                                </div>
                                <div class="row justify-content-center">
                                    <div class="col-lg-8">

                                        <fieldset>
                                            <legend>Personal Details</legend>

                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">First name</asp:Label>
                                                <asp:TextBox runat="server" ID="txtfname2" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvfname2" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtfname2" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="RequiredFieldValidator">First name is required </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Middle name</asp:Label>
                                                <asp:TextBox runat="server" ID="txtmname2" CssClass="form-control" />
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Last name</asp:Label>
                                                <asp:TextBox runat="server" ID="txtlname2" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvlname2" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtlname2" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="RequiredFieldValidator">Last name is required </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="ddlgender2" CssClass="form-label"> Gender</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlgender2" runat="server" name="gender" CssClass="form-control">
                                                    <asp:ListItem Text="Select gender" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvgender2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlgender2" InitialValue="" SetFocusOnError="true" ErrorMessage="Gender is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="mb-3">
                                                <div class="form-check-inline">
                                                    <asp:RadioButton runat="server" GroupName="gender" ID="radMr2" />
                                                    <asp:Label runat="server" CssClass="form-check-label">Mr</asp:Label>
                                                </div>
                                                <div class="form-check-inline">
                                                    <asp:RadioButton runat="server" GroupName="gender" ID="radMrs2" />
                                                    <asp:Label runat="server" CssClass="form-check-label">Mrs</asp:Label>
                                                </div>
                                                <div class="form-check-inline">
                                                    <asp:RadioButton runat="server" GroupName="gender" ID="radMiss2" />
                                                    <asp:Label runat="server" CssClass="form-check-label">Miss</asp:Label>
                                                </div>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Date of birth</asp:Label>
                                                <asp:TextBox runat="server" ID="txtdob2" name="dob" TextMode="Date" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvdob2" ValidationGroup="vgtutor" runat="server" ControlToValidate="txtdob2" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date of birth is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="rvdob2" runat="server" ValidationGroup="vgtutor" ControlToValidate="txtdob2" Display="Dynamic" Text="Invalid Age" ForeColor="Red" ErrorMessage="Invalid date of birth"></asp:RangeValidator>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="fuppicture3" CssClass="form-label">Upload profile Picture</asp:Label>
                                                <asp:FileUpload ID="fuppicture3" runat="server" name="profilePicture" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvpicture3" runat="server" Display="Dynamic" ValidationGroup="vgtutor" ControlToValidate="fuppicture3" SetFocusOnError="true" ErrorMessage="Picture is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>Education details</legend>

                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Bio</asp:Label>
                                                <asp:TextBox runat="server" ID="txtbio2" TextMode="MultiLine" CssClass="form-control" />
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="fuppicture4" CssClass="form-label">Upload your resume</asp:Label>
                                                <asp:FileUpload ID="fuppicture4" runat="server" name="certicatePicture" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvfuppicture4" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="fuppicture4" SetFocusOnError="true" ErrorMessage="Certificate is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>contact details</legend>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtemail2" CssClass="form-label">Email</asp:Label>
                                                <asp:TextBox runat="server" ID="txtemail2" name="email" CssClass="form-control" TextMode="Email" />
                                                <asp:RequiredFieldValidator ID="rfvemail2" ValidationGroup="vgtutor" Display="Dynamic" runat="server" ControlToValidate="txtemail2" SetFocusOnError="true" ErrorMessage="Email is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="regemail3" ControlToValidate="txtemail2" ValidationGroup="vgtutor" Display="Dynamic" ForeColor="Red" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$" runat="server" ErrorMessage="Invalid email"></asp:RegularExpressionValidator>
                                            </div>

                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtmob2" CssClass="form-label">Mobile number</asp:Label>
                                                <asp:TextBox runat="server" ID="txtmob2" name="mobileNumber" CssClass="form-control" onkeydown="return validateInput(event)" />
                                                <asp:RequiredFieldValidator ID="rfvmob2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtmob2" SetFocusOnError="true" ErrorMessage="Mobile number is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvmob2" runat="server" ForeColor="red" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtmob2" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Mobile number is invalid"></asp:CompareValidator>
                                                <asp:RegularExpressionValidator ID="regmob2" ControlToValidate="txtmob2" ValidationGroup="vgtutor" Display="Dynamic" ValidationExpression="\d{8}" ForeColor="Red" runat="server" ErrorMessage="Mobile number must have 8 digits"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">Country</asp:Label>
                                                <asp:DropDownList ID="ddlcountry2" runat="server" name="country">
                                                    <asp:ListItem Text="Select Country" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvcountry2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlcountry2" InitialValue="" SetFocusOnError="true" ErrorMessage="Country is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>

                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div class="mb-3">
                                                        <asp:Label runat="server" CssClass="form-label">District</asp:Label>
                                                        <asp:DropDownList ID="ddldistrict2" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged2">
                                                            <asp:ListItem Text="Select district" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvdistrict2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddldistrict2" InitialValue="-1" SetFocusOnError="true" ErrorMessage="District is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="mb-3">
                                                        <asp:Label runat="server" CssClass="form-label">Village/Town</asp:Label>
                                                        <asp:DropDownList ID="ddlvt2" runat="server">
                                                            <asp:ListItem Text="Select village/town" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvvt2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="ddlvt2" InitialValue="-1" SetFocusOnError="true" ErrorMessage="village/town is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddldistrict2" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                            <div class="mb-3">
                                                <asp:Label runat="server" CssClass="form-label">street Address</asp:Label>
                                                <asp:TextBox runat="server" ID="txtstreetaddress2" CssClass="form-control" />
                                            
                                            <asp:RequiredFieldValidator ID="rfvstreetaddress2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtstreetaddress2" InitialValue="" SetFocusOnError="true" ErrorMessage="Street address is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                </div>
                                        </fieldset>

                                        <fieldset>
                                            <legend>login details</legend>
                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtuname2" CssClass="form-label">Username</asp:Label>
                                                <asp:TextBox runat="server" ID="txtuname2" name="username2" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvuname2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtuname2" SetFocusOnError="true" ErrorMessage="Username is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtpass2" CssClass="form-label">Password</asp:Label>
                                                <asp:TextBox runat="server" ID="txtpass2" name="password" TextMode="Password" CssClass="form-control" />
                                                <asp:CustomValidator ID="cusvpass2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtpass2" ForeColor="Red" ClientValidationFunction="valPass_ClientValidate2" OnServerValidate="cusvpass_ServerValidate2" ErrorMessage="Password should be 7 to 12 characters"></asp:CustomValidator>

                                                <asp:RequiredFieldValidator ID="rfvpass2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtpass2" SetFocusOnError="true" ErrorMessage="Password is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                            </div>



                                            <div class="mb-3">
                                                <asp:Label runat="server" for="txtcpass2" CssClass="form-label">Confirm Password</asp:Label>
                                                <asp:TextBox runat="server" ID="txtcpass2" name="confirmPassword" TextMode="Password" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvcpass2" runat="server" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtcpass2" SetFocusOnError="true" ErrorMessage="Confirm Password is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvcpass2" runat="server" ForeColor="red" ValidationGroup="vgtutor" Display="Dynamic" ControlToValidate="txtcpass2" ControlToCompare="txtpass2" Operator="Equal" Type="String" ErrorMessage="Passwords do not match"></asp:CompareValidator>

                                                <div>
                                                    <asp:CheckBox ID="chkAgree2" runat="server" Text="Term & Conditions and Privacy Statement" />
                                                    <br />
                                                    <asp:CustomValidator ID="cvAgree2" runat="server" ValidationGroup="vgtutor"
                                                        ClientValidationFunction="validateCheckBox"
                                                        ErrorMessage="Please agree to the terms and conditions."
                                                        ForeColor="Red"
                                                        Display="Dynamic">
                                                    </asp:CustomValidator>
                                                </div>
                                            </div>

                                        </fieldset>
                                        <br />
                                        <div class="text-center">
                                            <asp:Button ID="registerButton2" runat="server" ValidationGroup="vgtutor" CssClass="btn btn-primary py-3 px-5" Text="Sign Up" OnClick="registerButton2_Click" />

                                        </div>
                                        <!-- Login link -->
                                        <div class="text-center mt-3">
                                            <p>Already have an account? <a href="login.aspx" class="text-primary">Log In</a></p>
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
                                <asp:Image ID="Image1" ImageUrl="~/img/feature.jpg" Width="100%" Height="100%" runat="server" CssClass="img-fluid" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-7">
        <div class="owl-carousel testimonial-carousel">
            <div class="bg-white p-5">
                <i class="fa fa-3x fa-quote-left text-primary mb-4"></i>
                <p>Sed et elitr ipsum labore dolor diam, ipsum duo vero sed sit est est ipsum eos clita est ipsum. Est nonumy tempor at kasd. Sed at dolor duo ut dolor, et justo erat dolor magna sed stet amet elitr duo lorem</p>
                <div class="d-flex flex-shrink-0 align-items-center mt-4">
                    <img class="img-fluid mr-4" src="img/testimonial-2.jpg" alt="">
                    <div>
                        <h5>Student Name</h5>
                        <span>Web Design</span>
                    </div>
                </div>
            </div>
            <div class="bg-white p-5">
                <i class="fa fa-3x fa-quote-left text-primary mb-4"></i>
                <p>Sed et elitr ipsum labore dolor diam, ipsum duo vero sed sit est est ipsum eos clita est ipsum. Est nonumy tempor at kasd. Sed at dolor duo ut dolor, et justo erat dolor magna sed stet amet elitr duo lorem</p>
                <div class="d-flex flex-shrink-0 align-items-center mt-4">
                    <img class="img-fluid mr-4" src="img/testimonial-1.jpg" alt="">
                    <div>
                        <h5>Student Name</h5>
                        <span>Web Design</span>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
