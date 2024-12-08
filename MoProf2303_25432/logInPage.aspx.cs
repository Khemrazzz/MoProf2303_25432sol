using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class logInPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if cookies exist and populate username and password fields
                if (Request.Cookies["studentUsername"] != null && Request.Cookies["studentPassword"] != null)
                {
                    txtuname.Text = Request.Cookies["studentUsername"].Value;
                    txtpass.Text = Request.Cookies["studentPassword"].Value;
                }
                else if (Request.Cookies["tutorUsername"] != null && Request.Cookies["tutorPassword"] != null)
                {
                    txtuname.Text = Request.Cookies["tutorUsername"].Value;
                    txtpass.Text = Request.Cookies["tutorPassword"].Value;
                }
            }
        }

        public string Encrypt(string cipherText)
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

        protected void sendMessageButton_Click(object sender, EventArgs e)
        {
            string uname = txtuname.Text.Trim();
            string password = txtpass.Text.Trim();
            bool rememberMe = chkRememberMe.Checked;

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                string encryptedPassword = Encrypt(password);
                // Check for student
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE Username=@Username AND Password=@Password", con))
                {
                    cmd.Parameters.AddWithValue("@Username", uname);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows && dr.Read())
                        {

                            // Check if account approval is pending
                            if (IsApprovalPending(uname))
                            {
                                lblmsg.Text = "Your account approval is pending. Please wait for approval.";
                                lblmsg.Visible = true;
                                return;
                            }

                            if (!IsAccountActive(uname, "Student"))
                            {
                                lblmsg.Text = "Your account is inactive. Please contact support.";
                                lblmsg.Visible = true;
                                LogLoginAttempt(uname, "Student", false);
                                return;
                            }

                            ResetFailedLoginAttempts(uname, "Student");
                            LogLoginAttempt(uname, "Student", true);

                            if (rememberMe)
                            {
                                Response.Cookies["studentUsername"].Value = uname;
                                Response.Cookies["studentPassword"].Value = password;
                                Response.Cookies["studentUsername"].Expires = DateTime.Now.AddDays(100);
                                Response.Cookies["studentPassword"].Expires = DateTime.Now.AddDays(100);
                            }
                            else
                            {
                                Response.Cookies["studentUsername"].Expires = DateTime.Now.AddDays(-100);
                                Response.Cookies["studentPassword"].Expires = DateTime.Now.AddDays(-100);
                            }

                            Session["studentUsername"] = uname;
                            Session["Student_Id"] = dr["Student_Id"].ToString();

                            string returnUrl = Session["ReturnUrl"] as string;
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                Response.Redirect(returnUrl);
                            }
                            else
                            {
                                Response.Redirect("~/studentDashboardPage.aspx");
                            }
                        }
                    }
                }

                // Check for tutor
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tutor WHERE Username=@Username AND Password=@Password", con))
                {
                    cmd.Parameters.AddWithValue("@Username", uname);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows && dr.Read())
                        {

                            // Check if account approval is pending
                            if (IsApprovalPending2(uname))
                            {
                                lblmsg.Text = "Your account approval is pending. Please wait for approval.";
                                lblmsg.Visible = true;
                                return;
                            }

                            if (!IsAccountActive(uname, "Tutor"))
                            {
                                lblmsg.Text = "Your account is inactive. Please contact support.";
                                lblmsg.Visible = true;
                                LogLoginAttempt(uname, "Tutor", false);
                                return;
                            }

                            ResetFailedLoginAttempts(uname, "Tutor");
                            LogLoginAttempt(uname, "Tutor", true);

                            if (rememberMe)
                            {
                                Response.Cookies["tutorUsername"].Value = uname;
                                Response.Cookies["tutorPassword"].Value = password;
                                Response.Cookies["tutorUsername"].Expires = DateTime.Now.AddDays(100);
                                Response.Cookies["tutorPassword"].Expires = DateTime.Now.AddDays(100);
                            }
                            else
                            {
                                Response.Cookies["tutorUsername"].Expires = DateTime.Now.AddDays(-100);
                                Response.Cookies["tutorPassword"].Expires = DateTime.Now.AddDays(-100);
                            }

                            Session["tutorUsername"] = uname;
                            Session["Tutor_Id"] = dr["Tutor_Id"].ToString();

                            string returnUrl = Session["ReturnUrl"] as string;
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                Response.Redirect(returnUrl);
                            }
                            else
                            {
                                Response.Redirect("~/tutorDashboardPage.aspx");
                            }
                        }
                        else
                        {
                            string checkstudenttype = Checkstudentlogintype(uname);
                            string checkTutorlogintype = CheckTutorlogintype(uname);
                            int failedLoginAttempts = 0;
                            if (checkstudenttype != null)
                            {
                                IncrementFailedLoginAttempts(uname, checkstudenttype);
                                failedLoginAttempts = GetFailedLoginAttempts(uname, checkstudenttype);

                                if (failedLoginAttempts >= 3)
                                {
                                    DeactivateAccount(uname, checkstudenttype);
                                    lblmsg.Text = "Too many failed login attempts. Your account has been locked. Please contact support.";
                                    lblmsg.Visible = true;
                                }
                                else
                                {
                                    lblmsg.Text = "Invalid Username or password.";
                                    lblmsg.Visible = true;
                                }
                                LogLoginAttempt(uname, checkstudenttype, false);
                            }
                            else if (checkTutorlogintype != null)
                            {
                                IncrementFailedLoginAttempts(uname, checkTutorlogintype);
                                failedLoginAttempts = GetFailedLoginAttempts(uname, checkTutorlogintype);

                                if (failedLoginAttempts >= 3)
                                {
                                    DeactivateAccount(uname, checkTutorlogintype);
                                    lblmsg.Text = "Too many failed login attempts. Your account has been locked. Please contact support.";
                                    lblmsg.Visible = true;
                                }
                                else
                                {
                                    lblmsg.Text = "Invalid Username or password.";
                                    lblmsg.Visible = true;
                                }
                                LogLoginAttempt(uname, checkTutorlogintype, false);
                            }

                        }
                    }
                }
            }
        }


        private void LogLoginAttempt(string username, string logintype, bool isSuccess)
        {
            string ipAddress = GetLocalIPAddress();
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "INSERT INTO LoginAudit (Username, AttemptTime, IPAddress, IsSuccessful, LoginType) VALUES (@username, @attemptTime, @ipAddress, @isSuccess, @logintype)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@attemptTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ipAddress", ipAddress);
                    cmd.Parameters.AddWithValue("@isSuccess", isSuccess);
                    cmd.Parameters.AddWithValue("@logintype", logintype);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool IsAccountActive(string username, string userType)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = $"SELECT IsActive FROM {userType} WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    return (bool)cmd.ExecuteScalar();
                }
            }
        }

        private void IncrementFailedLoginAttempts(string username, string userType)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = $"UPDATE {userType} SET FailedLoginAttempts = FailedLoginAttempts + 1 WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private int GetFailedLoginAttempts(string username, string userType)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = $"SELECT FailedLoginAttempts FROM {userType} WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private void ResetFailedLoginAttempts(string username, string userType)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = $"UPDATE {userType} SET FailedLoginAttempts = 0 WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeactivateAccount(string username, string userType)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = $"UPDATE {userType} SET IsActive = 0 WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateLastLoginDate(string userId, string userType)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = $"UPDATE {userType} SET LastLoginDate = @lastLoginDate WHERE {userType}_Id = @userId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@lastLoginDate", DateTime.Now);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static string GetLocalIPAddress()
        {
            string localIP = "Not available, please check your network settings!";
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return localIP;
        }

        private string Checkstudentlogintype(string uname)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = $"SELECT Username from Student where Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", uname);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return "Student";
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        private string CheckTutorlogintype(string uname)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = $"SELECT Username from Tutor where Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", uname);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return "Tutor";
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }


        private bool IsApprovalPending(string username)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "SELECT Approval FROM Student WHERE Username = @username AND Approval IS NOT NULL AND Approval <> 'Approved'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    return (result != null);
                }
            }
        }


        private bool IsApprovalPending2(string username)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "SELECT Approval FROM Tutor WHERE Username = @username AND Approval IS NOT NULL AND Approval <> 'Approved'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    return (result != null);
                }
            }
        }

    }
}