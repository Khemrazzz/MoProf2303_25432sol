using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

using Antlr.Runtime.Tree;

using System.Threading.Tasks;

namespace MoProf2303_25432
{
    public partial class adsRegistrationPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                rvrd.MinimumValue = DateTime.Now.ToShortDateString();
                rvrd.MaximumValue = DateTime.Now.ToShortDateString();
                rvrd.Type = ValidationDataType.Date;

                LoadCountry();
                //LoadDistrict();
                //LoadVT();
                PopulateDistrictList(ddldistrict);

            }
        }
        protected void registerButton3_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO Advertiser (CompanyName, RegistrationDate, ProfilePicture, BusinessPicture, Proposal, Email, MobileNumber, Country, District, VillageTown, StrAddress, Username, Password, CPassword, Website, Approval, IsActive ) " +
                    "VALUES (@CompanyName, @RegistrationDate, @ProfilePicture, @BusinessPicture, @Proposal, @Email, @MobileNumber, @Country, @District, @VillageTown, @StrAddress, @Username, @Password, @CPassword, @Website, @Approval, @IsActive)", con);


                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@CompanyName", txtcname.Text);
                    cmd.Parameters.AddWithValue("@RegistrationDate", txtreg.Text);
                    cmd.Parameters.AddWithValue("@Website", txturl.Text);
                    cmd.Parameters.AddWithValue("@Proposal", txtbio3.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@MobileNumber", txtmob.Text);
                    cmd.Parameters.AddWithValue("@Country", ddlcountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@District", ddldistrict.SelectedValue);
                    cmd.Parameters.AddWithValue("@VillageTown", ddlvt.SelectedValue);
                    cmd.Parameters.AddWithValue("@StrAddress", txtstreetaddress.Text);
                    cmd.Parameters.AddWithValue("@Username", txtuname.Text);
                    //cmd.Parameters.AddWithValue("@Password", txtpass.Text);
                    cmd.Parameters.AddWithValue("@Password", Encrypt(txtpass.Text));
                    cmd.Parameters.AddWithValue("@CPassword", Encrypt(txtcpass.Text));
                    //add a method to encrypt your password                     
                    //cmd.Parameters.AddWithValue("@Password", Encrypt(txtpass.Text));
                    //set the status to active or inactive 
                    
                    cmd.Parameters.AddWithValue("@Approval", "pending"); //admin

                    cmd.Parameters.AddWithValue("@IsActive", "True"); //freeze 


                    cmd.CommandType = CommandType.Text;

                    // Handle profile picture upload
                    if (fuppicture5.HasFile)
                    {
                        if (CheckFileType(fuppicture5.FileName))
                        {
                            string fileName = Path.GetFileName(fuppicture5.PostedFile.FileName);
                            string filePath = Server.MapPath("~/adsImages/" + fileName);
                            fuppicture5.SaveAs(filePath);

                            // Save file path to the database
                            cmd.Parameters.AddWithValue("@ProfilePicture", "~/adsImages/" + fileName);
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ProfilePicture", DBNull.Value);
                    }


                    if (fuppicture6.HasFile)
                    {
                        if (CheckFileType(fuppicture6.FileName))
                        {
                            string fileName = Path.GetFileName(fuppicture6.PostedFile.FileName);
                            string filePath = Server.MapPath("~/adsImages/" + fileName);
                            fuppicture6.SaveAs(filePath);

                            // Save file path to the database
                            cmd.Parameters.AddWithValue("@BusinessPicture", "~/adsImages/" + fileName);
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BusinessPicture", DBNull.Value);
                    }

                    sendemail();
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // Redirect to thank you page after successful registration
                Response.Redirect("adslogInPage.aspx");
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

        /** protected void LoadDistrict()
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
         } **/

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
            if (fuppicture5.HasFile)
            {
                if (CheckFileType(fuppicture5.FileName))
                {
                    filen = Path.GetFileName(fuppicture5.PostedFile.FileName);
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
                    m.Attachments.Add(new Attachment(Server.MapPath("~/adsImages/") + filen));
                    msgBody.Append("<a href='https://" + HttpContext.Current.Request.Url.Authority + "/adsLogInPage'>Use this link to log-in once your account is activated.</ a > ");
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
                dropdown.Items.Insert(0, new ListItem("Select Village/Town", "-1"));
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
                ddlvt.Items.Insert(0, new ListItem("Select Village/Town", "-1"));
            }
        }
    }
}