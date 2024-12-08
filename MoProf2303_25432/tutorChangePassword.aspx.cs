using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class tutorChangePassword : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                if (string.IsNullOrEmpty(Convert.ToString(Session["Tutor_Id"])))
                {
                    Response.Redirect("LogInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else if (!IsPostBack)
                {
                    if (Session["Tutor_Id"] != null)
                    {
                        string Tutor_Id = Session["Tutor_Id"].ToString();

                        PasswordTutorEdit(Tutor_Id);

                    }
                }
            }
        }




        private void PasswordTutorEdit(string TutorId)
        {

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string Tutor_Id = Session["Tutor_Id"].ToString();
            string currentPassword = txtcurrentpass.Text;
            string newPassword = txtpass2.Text;
            string confirmPassword = txtcpass2.Text;

            if (newPassword != confirmPassword)
            {

                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Password do not match.";
            }


            string connectionString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string query = "SELECT COUNT(*) FROM Tutor WHERE Tutor_ID = @Tutor_ID AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tutor_ID", Tutor_Id);
                    command.Parameters.AddWithValue("@Password", Encrypt2(currentPassword));

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {

                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "change Password unsuccessful.";
                        return;
                    }
                }


                query = "UPDATE Tutor SET Password = @NewPassword WHERE Tutor_ID = @Tutor_ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewPassword", Encrypt(newPassword));
                    command.Parameters.AddWithValue("@Tutor_Id", Tutor_Id);

                    command.ExecuteNonQuery();


                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Password updated successfully.";
                }
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


        public string Encrypt2(string cipherText)
        {
            // defining encrytion key 
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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
                    cipherText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return cipherText;
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