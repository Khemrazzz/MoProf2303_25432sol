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
    public partial class StudentChangePassword : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Student_Id"])))
                {
                    Response.Redirect("LogInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else if (Session["Student_Id"] != null)
                {
                    string Student_Id = Session["Student_Id"].ToString();
                    PasswordStudentEdit(Student_Id);
                }
            }
        }

        private void PasswordStudentEdit(string Student_Id)
        {
            // Add any necessary logic to pre-load data if needed.
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string Student_Id = Session["Student_Id"]?.ToString();
            if (Student_Id == null)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Student session expired. Please log in again.";
                return;
            }

            string currentPassword = txtcurrentpass.Text;
            string newPassword = txtpass2.Text;
            string confirmPassword = txtcpass2.Text;

            if (newPassword != confirmPassword)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            using (SqlConnection connection = new SqlConnection(_conString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Student WHERE Student_Id = @Student_Id AND Password = @CurrentPassword";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Student_Id", Student_Id);
                    command.Parameters.AddWithValue("@CurrentPassword", Encrypt2(currentPassword));

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Current password is incorrect.";
                        return;
                    }
                }

                query = "UPDATE Student SET Password = @NewPassword WHERE Student_Id = @Student_Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewPassword", Encrypt(newPassword));
                    command.Parameters.AddWithValue("@Student_Id", Student_Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Password updated successfully.";
                    }
                    else
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Error updating password.";
                    }
                }
            }
        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
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
            if (args.Value.Length < 7 || args.Value.Length > 12)
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
