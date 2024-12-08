<%@ Page Title="" Language="C#" MasterPageFile="~/userMaster.Master" AutoEventWireup="true" CodeBehind="adsRegistrationPage.aspx.cs" Inherits="MoProf2303_25432.adsRegistrationPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validateCheckBox(sender, args) {
            var chkAgree = document.getElementById('<%= chkAgree.ClientID %>');
            args.IsValid = chkAgree.checked;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="section-title text-center">
        <h3>Welcome to MoProf</h3>
        <p>Your ideal partner for promoting your business!</p>
    </div>

    <hr class="invis">
    <div class="row">
        <!-- Form on the left side -->
        <div class="col-md-7">
            <div class="container py-5">
                <div class="text-center mb-5">
                    <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">Registration</h5>
                    <h1>Sign up</h1>
                </div>
                <div class="row justify-content-center">
                    <div class="col-lg-8">
                        <fieldset>
                            <legend>Company details</legend>
                            <div class="mb-3">
                                <asp:Label runat="server" CssClass="form-label">Company name</asp:Label>
                                <asp:TextBox runat="server" ID="txtcname" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvcname" runat="server" ValidationGroup="vgads" ControlToValidate="txtcname" Display="Dynamic" SetFocusOnError="true" ForeColor="red" ErrorMessage="RequiredFieldValidator">Company name is required </asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <asp:Label runat="server" CssClass="form-label">Date of registration</asp:Label>
                                <asp:TextBox runat="server" ID="txtreg" name="reg" TextMode="Date" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvreg" runat="server" ValidationGroup="vgads" ControlToValidate="txtreg" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Registration date is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                               <asp:RangeValidator ID="rvrd" runat="server" ValidationGroup="vgads" ControlToValidate="txtreg" Display="Dynamic" Text="Invalid date" ForeColor="Red" ErrorMessage="Invalid date"></asp:RangeValidator>

                            </div>
                            <div class="mb-3">
                                <asp:Label runat="server"
                                    CssClass="form-label">Your Website URL</asp:Label>

                                <asp:TextBox runat="server" ID="txturl"
                                    CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvurl" runat="server" ValidationGroup="vgads" ControlToValidate="txturl" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Url is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regurl" ControlToValidate="txturl" ValidationGroup="vgads" Display="Dynamic" ForeColor="Red" ValidationExpression="^(https?://)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(/.*)?$" runat="server" ErrorMessage="Invalid url"></asp:RegularExpressionValidator>

                            </div>

                            <div class="mb-3">
                                <asp:Label runat="server" for="fuppicture5" CssClass="form-label">Upload profile picture</asp:Label>
                                <asp:FileUpload ID="fuppicture5" runat="server" name="profilePicture" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvpicture5" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="fuppicture5" SetFocusOnError="true" ErrorMessage="Picture is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <asp:Label runat="server" for="fuppicture6" CssClass="form-label">Upload business certificate</asp:Label>
                                <asp:FileUpload ID="fuppicture6" runat="server" name="profilePicture" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvpicture6" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="fuppicture6" SetFocusOnError="true" ErrorMessage="Certificate is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <asp:Label runat="server" CssClass="form-label">Your proposal</asp:Label>
                                <asp:TextBox runat="server" ID="txtbio3" TextMode="MultiLine" CssClass="form-control" />
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Contact details</legend>
                            <div class="mb-3">
                                <asp:Label runat="server" for="txtemail" CssClass="form-label">Email</asp:Label>
                                <asp:TextBox runat="server" ID="txtemail" name="email" CssClass="form-control" TextMode="Email" />
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtemail" SetFocusOnError="true" ErrorMessage="Email is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regemail" ControlToValidate="txtemail" ValidationGroup="vgads" Display="Dynamic" ForeColor="Red" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$" runat="server" ErrorMessage="Invalid email"></asp:RegularExpressionValidator>
                            </div>

                            <div class="mb-3">
                                <asp:Label runat="server" for="txtmob" CssClass="form-label">Mobile number</asp:Label>
                                <asp:TextBox runat="server" ID="txtmob" name="mobileNumber" CssClass="form-control" onkeydown="return validateInput(event)" />
                                <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtmob" SetFocusOnError="true" ErrorMessage="Mobile number is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvmob" runat="server" ForeColor="red" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtmob" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Mobile number is invalid"></asp:CompareValidator>
                                <asp:RegularExpressionValidator ID="regmob" ControlToValidate="txtmob" ValidationGroup="vgads" Display="Dynamic" ValidationExpression="\d{8}" ForeColor="Red" runat="server" ErrorMessage="Mobile number must have 8 digits"></asp:RegularExpressionValidator>
                            </div>
                            <div class="mb-3">
                                <asp:Label runat="server" CssClass="form-label">Country</asp:Label>
                                <asp:DropDownList ID="ddlcountry" runat="server" name="country">
                                    <asp:ListItem Text="Select country" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvcountry" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="ddlcountry" InitialValue="" SetFocusOnError="true" ErrorMessage="Country is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="mb-3">
                                        <asp:Label runat="server" CssClass="form-label">District</asp:Label>
                                        <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                            <asp:ListItem Text="Select district" Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdistrict" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="ddldistrict" InitialValue="-1" SetFocusOnError="true" ErrorMessage="District is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="mb-3">
                                        <asp:Label runat="server" CssClass="form-label">Village/Town</asp:Label>
                                        <asp:DropDownList ID="ddlvt" runat="server">
                                            <asp:ListItem Text="Select village/town" Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvvt" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="ddlvt" InitialValue="-1" SetFocusOnError="true" ErrorMessage="Village/Town is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddldistrict" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="mb-3">
                                <asp:Label runat="server" CssClass="form-label">Street address</asp:Label>
                                <asp:TextBox runat="server" ID="txtstreetaddress" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvstreetaddress" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtstreetaddress" InitialValue="" SetFocusOnError="true" ErrorMessage="Street address is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Login details</legend>
                            <div class="mb-3">
                                <asp:Label runat="server" for="txtuname" CssClass="form-label">Username</asp:Label>
                                <asp:TextBox runat="server" ID="txtuname" name="username" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtuname" SetFocusOnError="true" ErrorMessage="Username is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                            </div>

                            <div class="mb-3">
                                <asp:Label runat="server" for="txtpass" CssClass="form-label">Password</asp:Label>
                                <asp:TextBox runat="server" ID="txtpass" name="password" TextMode="Password" CssClass="form-control" />
                                <asp:CustomValidator ID="cusvpass" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtpass" ForeColor="Red" ClientValidationFunction="valPass_ClientValidate" OnServerValidate="cusvpass_ServerValidate" ErrorMessage="Password should be 7 to 12 characters"></asp:CustomValidator>
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtpass" SetFocusOnError="true" ErrorMessage="Password is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                            </div>



                            <div class="mb-3">
                                <asp:Label runat="server" for="txtcpass" CssClass="form-label">Confirm password</asp:Label>
                                <asp:TextBox runat="server" ID="txtcpass" name="confirmPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtcpass" SetFocusOnError="true" ErrorMessage="Confirm Password is mandatory" ForeColor="red"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvPassword" runat="server" ForeColor="red" ValidationGroup="vgads" Display="Dynamic" ControlToValidate="txtcpass" ControlToCompare="txtpass" Operator="Equal" Type="String" ErrorMessage="Passwords do not match"></asp:CompareValidator>

                                <div>
                                    <asp:CheckBox ID="chkAgree" runat="server" Text="Term & Conditions and Privacy Statement" />
                                    <br />
                                    <asp:CustomValidator ID="cvAgree" runat="server" ValidationGroup="vgads"
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
                            <asp:Button ID="registerButton3" runat="server" ValidationGroup="vgads" CssClass="btn btn-primary py-3 px-5" Text="Sign Up" OnClick="registerButton3_Click" />
                        </div>
                        <!-- Login link -->
                        <div class="text-center mt-3">
                            <p>Already have an account? <a href="adsLogInPage.aspx" class="text-primary">Log In</a></p>
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