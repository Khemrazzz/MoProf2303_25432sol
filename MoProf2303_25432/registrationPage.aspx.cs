using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class registrationPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                rvdob.MinimumValue = DateTime.Now.AddYears(-65).ToShortDateString();
                rvdob.MaximumValue = DateTime.Now.AddYears(-10).ToShortDateString();
                rvdob.Type = ValidationDataType.Date;

                rvdob2.MinimumValue = DateTime.Now.AddYears(-65).ToShortDateString();
                rvdob2.MaximumValue = DateTime.Now.AddYears(-10).ToShortDateString();
                rvdob2.Type = ValidationDataType.Date;

                LoadCountry();
                LoadCountry2();
                LoadGender();
                LoadGender2();
                //LoadDistrict();
                //LoadDistrict2();
                //LoadVT();
                //LoadVT2();
                //LoadStatus();
                //LoadGrade();
                PopulateEducationStatusList(ddlcsts);
                PopulateDistrictList(ddldistrict);
                PopulateDistrictList2(ddldistrict2);

                

            }
        }

        //tempodis

        /**   protected void LoadStatus()
           {
               string query = "SELECT Sts_Id, Current_Status FROM [dbo].[Education_Status]";

               using (SqlConnection conn = new SqlConnection(_conString))
               {
                   using (SqlCommand cmd = new SqlCommand(query, conn))
                   {
                       conn.Open();
                       SqlDataReader reader = cmd.ExecuteReader();
                       ddlcsts.DataSource = reader;
                       ddlcsts.DataTextField = "Current_Status";
                       ddlcsts.DataValueField = "Sts_Id";
                       ddlcsts.DataBind();
                       reader.Close();
                   }
               }

               ddlcsts.Items.Insert(0, new ListItem("Select status", ""));

           }
           protected void LoadGrade()
           {
               string query = "SELECT Grade_Id, Grade_Name FROM [dbo].[Education_Grade]";

               using (SqlConnection conn = new SqlConnection(_conString))
               {
                   using (SqlCommand cmd = new SqlCommand(query, conn))
                   {
                       conn.Open();
                       SqlDataReader reader = cmd.ExecuteReader();
                       ddlgrade.DataSource = reader;
                       ddlgrade.DataTextField = "Grade_Name";
                       ddlgrade.DataValueField = "Grade_Id";
                       ddlgrade.DataBind();
                       reader.Close();
                   }
               }

               ddlgrade.Items.Insert(0, new ListItem("Select grade", ""));

           } */

        protected void LoadCountry()
        {
            string query = "SELECT Country_Id, Country_Name FROM [dbo].[Country]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlcountry.DataSource = reader;
                    ddlcountry.DataTextField = "Country_Name";
                    ddlcountry.DataValueField = "Country_Id";
                    ddlcountry.DataBind();
                    reader.Close();
                }
            }

            ddlcountry.Items.Insert(0, new ListItem("Select country", ""));
        }

        protected void LoadCountry2()
        {
            string query = "SELECT Country_Id, Country_Name FROM [dbo].[Country]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlcountry2.DataSource = reader;
                    ddlcountry2.DataTextField = "Country_Name";
                    ddlcountry2.DataValueField = "Country_Id";
                    ddlcountry2.DataBind();
                    reader.Close();
                }
            }

            ddlcountry2.Items.Insert(0, new ListItem("Select country", ""));
        }


        protected void LoadGender()
        {
            string query = "SELECT Gender_Id, Gender FROM [dbo].[Gender]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlgender.DataSource = reader;
                    ddlgender.DataTextField = "Gender";
                    ddlgender.DataValueField = "Gender_Id";
                    ddlgender.DataBind();
                    reader.Close();
                }
            }

            ddlgender.Items.Insert(0, new ListItem("Select gender", ""));
        }

        protected void LoadGender2()
        {
            string query = "SELECT Gender_Id, Gender FROM [dbo].[Gender]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlgender2.DataSource = reader;
                    ddlgender2.DataTextField = "Gender";
                    ddlgender2.DataValueField = "Gender_Id";
                    ddlgender2.DataBind();
                    reader.Close();
                }
            }

            ddlgender2.Items.Insert(0, new ListItem("Select gender", ""));
        }

      /**  protected void LoadDistrict()
        {
            string query = "SELECT District_Id, District_Name FROM [dbo].[District]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddldistrict.DataSource = reader;
                    ddldistrict.DataTextField = "District_Name";
                    ddldistrict.DataValueField = "District_Id";
                    ddldistrict.DataBind();
                    reader.Close();
                }
            }

            ddldistrict.Items.Insert(0, new ListItem("Select district", ""));
        }

        protected void LoadDistrict2()
        {
            string query = "SELECT District_Id, District_Name FROM [dbo].[District]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddldistrict2.DataSource = reader;
                    ddldistrict2.DataTextField = "District_Name";
                    ddldistrict2.DataValueField = "District_Id";
                    ddldistrict2.DataBind();
                    reader.Close();
                }
            }

            ddldistrict2.Items.Insert(0, new ListItem("Select district", ""));
        }

        protected void LoadVT()
        {
            string query = "SELECT VT_Id, VT_Name FROM [dbo].[Village_Town]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlvt.DataSource = reader;
                    ddlvt.DataTextField = "VT_Name";
                    ddlvt.DataValueField = "VT_Id";
                    ddlvt.DataBind();
                    reader.Close();
                }
            }

            ddlvt.Items.Insert(0, new ListItem("Select village/town", ""));
        }

        protected void LoadVT2()
        {
            string query = "SELECT VT_Id, VT_Name FROM [dbo].[Village_Town]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlvt2.DataSource = reader;
                    ddlvt2.DataTextField = "VT_Name";
                    ddlvt2.DataValueField = "VT_Id";
                    ddlvt2.DataBind();
                    reader.Close();
                }
            }

            ddlvt2.Items.Insert(0, new ListItem("Select village/town", ""));
        } */

        protected void registerButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!chkAgree.Checked)
                {
                    cvAgree.IsValid = false;
                }
                else
                {
                    cvAgree.IsValid = true;
                }
                string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    var test = con.State.ToString();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Student (FirstName, MiddleName, LastName, Gender, GTag, DateOfBirth, ProfilePicture, EduStatus, EduGrade, Bio, CertificatePicture, Email, MobileNumber,Country, District, VillageTown, StrAddress, Username, Password, CPassword, Approval, IsActive) " +
                    "VALUES (@FirstName, @MiddleName, @LastName, @Gender, @GTag, @DateOfBirth, @ProfilePicture, @EduStatus, @EduGrade, @Bio, @CertificatePicture, @Email, @MobileNumber, @Country, @District, @VillageTown, @StrAddress, @Username, @Password, @CPassword, @Approval, @IsActive)", con);


                    // Determine selected gender
                    string gender = "";
                    if (radMr.Checked)
                    {
                        gender = "Mr";
                    }
                    else if (radMiss.Checked)
                    {
                        gender = "Miss";
                    }
                    else if (radMrs.Checked)
                    {
                        gender = "Mrs";
                    }

                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@FirstName", txtfname.Text);
                    cmd.Parameters.AddWithValue("@MiddleName", txtmname.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtlname.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlgender.SelectedValue);
                    cmd.Parameters.AddWithValue("@GTag", gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", txtdob.Text);
                    cmd.Parameters.AddWithValue("@EduStatus", ddlcsts.SelectedValue);
                    cmd.Parameters.AddWithValue("@EduGrade", ddlgrade.SelectedValue);
                    cmd.Parameters.AddWithValue("@Bio", txtbio.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@MobileNumber", txtmob.Text);
                    cmd.Parameters.AddWithValue("@Country", ddlcountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@District", ddldistrict.SelectedValue);
                    cmd.Parameters.AddWithValue("@VillageTown", ddlvt.SelectedValue);
                    cmd.Parameters.AddWithValue("@StrAddress", txtstreetaddress.Text);
                    cmd.Parameters.AddWithValue("@Username", txtuname.Text);
                    cmd.Parameters.AddWithValue("@Password", Encrypt(txtpass.Text));
                    cmd.Parameters.AddWithValue("@CPassword", Encrypt(txtcpass.Text));

                    cmd.Parameters.AddWithValue("@Approval", "pending");
                    cmd.Parameters.AddWithValue("@IsActive", "True");

                    // Handle profile picture upload
                    if (fuppicture.HasFile)
                    {
                        if (CheckFileType(fuppicture.FileName))
                        {
                            string fileName = Path.GetFileName(fuppicture.PostedFile.FileName);
                            string filePath = Server.MapPath("~/stdImages/" + fileName);
                            fuppicture.SaveAs(filePath);

                            // Save file path to the database
                            cmd.Parameters.AddWithValue("@ProfilePicture", "~/stdImages/" + fileName);
                        }

                        else
                        {
                            cmd.Parameters.AddWithValue("@ProfilePicture", DBNull.Value);
                        }
                    }


                    if (fuppicture2.HasFile)
                    {
                        if (CheckFileType(fuppicture2.FileName))
                        {
                            string fileName = Path.GetFileName(fuppicture2.PostedFile.FileName);
                            string filePath = Server.MapPath("~/stdImages/" + fileName);
                            fuppicture2.SaveAs(filePath);

                            // Read file content
                            byte[] fileContent = File.ReadAllBytes(filePath);

                            // Save file path to the database
                            cmd.Parameters.AddWithValue("@CertificatePicture", "~/stdImages/" + fileName);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CertificatePicture", DBNull.Value);
                        }
                    }

                        sendemail();
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // Redirect to thank you page after successful registration
                Response.Redirect("logInPage.aspx");
            }
        }

        protected void registerButton2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!chkAgree.Checked)
                {
                    cvAgree.IsValid = false;
                }
                else
                {
                    cvAgree.IsValid = true;
                }
                string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    var test = con.State.ToString();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Tutor (FirstName, MiddleName, LastName, Gender, GTag, DateOfBirth, ProfilePicture, Bio, ResumePicture, Email, MobileNumber, Country, District, VillageTown, StrAddress, Username, Password, CPassword, Status, Approval, IsActive) " +
                    "VALUES (@FirstName, @MiddleName, @LastName, @Gender, @GTag, @DateOfBirth, @ProfilePicture, @Bio, @ResumePicture, @Email, @MobileNumber, @Country, @District, @VillageTown, @StrAddress, @Username, @Password, @CPassword, @Status, @Approval, @IsActive)", con);


                    // Determine selected gender
                    string gender2 = "";
                    if (radMr.Checked)
                    {
                        gender2 = "Mr";
                    }
                    else if (radMiss.Checked)
                    {
                        gender2 = "Miss";
                    }
                    else if (radMrs.Checked)
                    {
                        gender2 = "Mrs";
                    }

                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@FirstName", txtfname2.Text);
                    cmd.Parameters.AddWithValue("@MiddleName", txtmname2.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtlname2.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlgender2.SelectedValue);
                    cmd.Parameters.AddWithValue("@GTag", gender2);
                    cmd.Parameters.AddWithValue("@DateOfBirth", txtdob.Text);
                    cmd.Parameters.AddWithValue("@Bio", txtbio2.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail2.Text);
                    cmd.Parameters.AddWithValue("@MobileNumber", txtmob2.Text);
                    cmd.Parameters.AddWithValue("@Country", ddlcountry2.SelectedValue);
                    cmd.Parameters.AddWithValue("@District", ddldistrict2.SelectedValue);
                    cmd.Parameters.AddWithValue("@VillageTown", ddlvt2.SelectedValue);
                    cmd.Parameters.AddWithValue("@StrAddress", txtstreetaddress2.Text);
                    cmd.Parameters.AddWithValue("@Username", txtuname2.Text);
                    cmd.Parameters.AddWithValue("@Password", Encrypt(txtpass2.Text));
                    cmd.Parameters.AddWithValue("@CPassword", Encrypt(txtcpass2.Text));

                    cmd.Parameters.AddWithValue("@status", "Inactive");
                   
                    cmd.Parameters.AddWithValue("@Approval", "pending");
                    cmd.Parameters.AddWithValue("@IsActive", "True");

                    // Handle profile picture upload
                    if (fuppicture3.HasFile)
                    {
                        if (CheckFileType(fuppicture3.FileName))
                        {
                            string fileName = Path.GetFileName(fuppicture3.PostedFile.FileName);
                            string filePath = Server.MapPath("~/tImages/" + fileName);
                            fuppicture3.SaveAs(filePath);

                            // Read file content
                            byte[] fileContent = File.ReadAllBytes(filePath);

                            // Save file path to the database
                            cmd.Parameters.AddWithValue("@ProfilePicture", "~/tImages/" + fileName);
                        }

                        else
                        {
                            cmd.Parameters.AddWithValue("@ProfilePicture", DBNull.Value);
                        }
                    }


                    if (fuppicture4.HasFile)
                    {
                        if (CheckFileType(fuppicture4.FileName))
                        {
                            string fileName = Path.GetFileName(fuppicture4.PostedFile.FileName);
                            string filePath = Server.MapPath("~/tImages/" + fileName);
                            fuppicture4.SaveAs(filePath);

                            // Read file content
                            byte[] fileContent = File.ReadAllBytes(filePath);

                            // Save file path to the database
                            cmd.Parameters.AddWithValue("@ResumePicture", "~/tImages/" + fileName);
                        }

                        else
                        {
                            cmd.Parameters.AddWithValue("@ResumePicture", DBNull.Value);
                        }
                    }

                    sendemail2();
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // Redirect to thank you page after successful registration
                Response.Redirect("logInPage.aspx");
            }
        }


        private void PopulateEducationStatusList(DropDownList dropdown)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("ViewEducationStatus", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "Current_Status";
                dropdown.DataValueField = "Sts_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Choose status", "-1"));
            }
        }

        private void PopulateGradeList(DropDownList dropdown, int Sts_Id)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("ViewGradeByStatus", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                scmd.Parameters.AddWithValue("@Sts_Id", Sts_Id);
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "Grade_Name";
                dropdown.DataValueField = "Grade_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Choose grade", "-1"));
            }
        }

        protected void ddlcsts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlcsts.SelectedValue, out int Sts_Id) && Sts_Id != -1)
            {
                PopulateGradeList(ddlgrade, Sts_Id);
            }
            else
            {
                ddlgrade.Items.Clear();
                ddlgrade.Items.Insert(0, new ListItem("Choose grade", "-1"));
            }
        }

        private void PopulateDistrictList(DropDownList dropdown)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("GetAllDistricts", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "District_Name";
                dropdown.DataValueField = "District_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Select district", "-1"));
            }
        }

        private void PopulateVillageTownList(DropDownList dropdown, int districtId)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("GetVillagesTownsByDistrict", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                scmd.Parameters.AddWithValue("@DistrictId", districtId);
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "VT_Name";
                dropdown.DataValueField = "VT_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Select village/town", "-1"));
            }
        }

        protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddldistrict.SelectedValue, out int districtId) && districtId != -1)
            {
                PopulateVillageTownList(ddlvt, districtId);
            }
            else
            {
                ddlvt.Items.Clear();
                ddlvt.Items.Insert(0, new ListItem("Select village/town", "-1"));
            }
        }


        private void PopulateDistrictList2(DropDownList dropdown)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("GetAllDistricts", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "District_Name";
                dropdown.DataValueField = "District_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Select district", "-1"));
            }
        }

        private void PopulateVillageTownList2(DropDownList dropdown, int districtId)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("GetVillagesTownsByDistrict", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                scmd.Parameters.AddWithValue("@DistrictId", districtId);
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "VT_Name";
                dropdown.DataValueField = "VT_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Select village/town", "-1"));
            }
        }

        protected void ddldistrict_SelectedIndexChanged2(object sender, EventArgs e)
        {
            if (int.TryParse(ddldistrict2.SelectedValue, out int districtId) && districtId != -1)
            {
                PopulateVillageTownList2(ddlvt2, districtId);
            }
            else
            {
                ddlvt2.Items.Clear();
                ddlvt2.Items.Insert(0, new ListItem("Select village/town", "-1"));
            }
        }

        public string Encrypt(string clearText)
        {
            // defining encrytion key 
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new
    byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65,
0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    // encoding using key 
                    using (CryptoStream cs = new CryptoStream(ms,
    encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        //Sending an email to notify and welcome user 
        private void sendemail()
        {
            string filen = "avatar.jpg";
            //Check whether the fileupload contains a file 
            if (fuppicture.HasFile)
            {
                if (CheckFileType(fuppicture.FileName))
                {
                    filen = Path.GetFileName(fuppicture.PostedFile.FileName);
                }
            }
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage m = new MailMessage(smtpSection.From, txtemail.Text.Trim()))
            {
                SmtpClient sc = new SmtpClient();
                try
                {
                    m.Subject = "Welcome aboard";
                    m.IsBodyHtml = true;
                    StringBuilder msgBody = new StringBuilder();
                    msgBody.Append("Dear " + txtuname.Text + ", your registration is successful, thank you for signing up");

                    //msgBody.Append(txtbody.Text.Trim()); 
                    m.Attachments.Add(new Attachment(Server.MapPath("~/stdImages/") + filen));
                    msgBody.Append("<a href='https://" + HttpContext.Current.Request.Url.Authority + "/LogInPage.aspx'>Use this link to log-in once your account is activated.</ a > ");
                    m.Body = msgBody.ToString();
                    sc.Host = smtpSection.Network.Host;
                    sc.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    sc.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    sc.Credentials = networkCred;
                    sc.Port = smtpSection.Network.Port;
                    sc.Send(m);

                    Response.Write("Email Sent successfully");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        //Sending an email to notify and welcome user 
        private void sendemail2()
        {
            string filen = "avatar.jpg";
            //Check whether the fileupload contains a file 
            if (fuppicture3.HasFile)
            {
                if (CheckFileType(fuppicture3.FileName))
                {
                    filen = Path.GetFileName(fuppicture3.PostedFile.FileName);
                }
            }
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage m = new MailMessage(smtpSection.From, txtemail2.Text.Trim()))
            {
                SmtpClient sc = new SmtpClient();
                try
                {
                    m.Subject = "Welcome aboard";
                    m.IsBodyHtml = true;
                    StringBuilder msgBody = new StringBuilder();
                    msgBody.Append("Dear " + txtuname2.Text + ", your registration is successful, thank you for signing up");

                    //msgBody.Append(txtbody.Text.Trim()); 
                    m.Attachments.Add(new Attachment(Server.MapPath("~/tImages/") + filen));
                    msgBody.Append("<a href='https://" + HttpContext.Current.Request.Url.Authority + "/LogInPage.aspx'>Use this link to log-in once your account is activated.</ a > ");
                    m.Body = msgBody.ToString();
                    sc.Host = smtpSection.Network.Host;
                    sc.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    sc.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    sc.Credentials = networkCred;
                    sc.Port = smtpSection.Network.Port;
                    sc.Send(m);

                    Response.Write("Email Sent successfully");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }


        bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        protected void cusvpass_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.Length <= 7 || args.Value.Length >= 12)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void cusvpass_ServerValidate2(object source, ServerValidateEventArgs args)
        {
            if (args.Value.Length <= 7 || args.Value.Length >= 12)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }


    }
}
