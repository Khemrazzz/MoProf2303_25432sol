using System;
using System.Data.SqlClient;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

namespace MoProf2303_25432
{
    public partial class adsLogInPage : Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if cookies exist and populate username and password fields
                if (Request.Cookies["advertiserUsername"] != null && Request.Cookies["advertiserPassword"] != null)
                {
                    txtuname.Text = Request.Cookies["advertiserUsername"].Value;
                    txtpass.Text = Request.Cookies["advertiserPassword"].Value;
                }
            }
        }

        public string Encrypt(string cipherText)
        {
            // defining encryption key 
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
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
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

            // Check if account approval is pending
            if (IsApprovalPending(uname))
            {
                lblmsg.Text = "Your account approval is pending. Please wait for approval.";
                lblmsg.Visible = true;
                return;
            }

            if (!IsAccountActive(uname))
            {
                lblmsg.Text = "Your account is inactive. Please contact support.";
                lblmsg.Visible = true;
                LogLoginAttempt(uname, "Advertiser", false);
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                // Check for advertiser
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Advertiser WHERE Username=@Username AND Password=@Password", con))
                {
                    cmd.Parameters.AddWithValue("@Username", uname);
                    cmd.Parameters.AddWithValue("@Password", Encrypt(password));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows && dr.Read())
                        {
                            // Advertiser login successful
                            ResetFailedLoginAttempts(uname);
                            LogLoginAttempt(uname, "Advertiser", true);

                            if (rememberMe)
                            {
                                Response.Cookies["advertiserUsername"].Value = uname;
                                Response.Cookies["advertiserPassword"].Value = password;
                                Response.Cookies["advertiserUsername"].Expires = DateTime.Now.AddDays(100);
                                Response.Cookies["advertiserPassword"].Expires = DateTime.Now.AddDays(100);
                            }
                            else
                            {
                                Response.Cookies["advertiserUsername"].Expires = DateTime.Now.AddDays(-100);
                                Response.Cookies["advertiserPassword"].Expires = DateTime.Now.AddDays(-100);
                            }

                            Session["advertiserUsername"] = uname;
                            Session["Advertiser_Id"] = dr["Advertiser_Id"].ToString();

                            // Redirect to the original page or the default page
                            string returnUrl = Session["ReturnUrl"] as string;
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                Response.Redirect(returnUrl);
                            }
                            else
                            {
                                Response.Redirect("~/adsDashboardPage.aspx");
                            }
                        }
                        else
                        {
                            IncrementFailedLoginAttempts(uname);
                            int failedLoginAttempts = GetFailedLoginAttempts(uname);

                            if (failedLoginAttempts >= 3)
                            {
                                DeactivateAccount(uname);
                                lblmsg.Text = "Too many failed login attempts. Your account has been locked. Please contact support.";
                                lblmsg.Visible = true;
                            }
                            else
                            {
                                lblmsg.Text = "Invalid Username or password.";
                                lblmsg.Visible = true;
                            }
                            LogLoginAttempt(uname, "Advertiser", false);
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

        private bool IsAccountActive(string username)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "SELECT isActive FROM Advertiser WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    return (bool)cmd.ExecuteScalar();
                }
            }
        }

        private void IncrementFailedLoginAttempts(string username)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "UPDATE Advertiser SET FailedLoginAttempts = FailedLoginAttempts + 1 WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private int GetFailedLoginAttempts(string username)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "SELECT FailedLoginAttempts FROM Advertiser WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private void ResetFailedLoginAttempts(string username)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "UPDATE Advertiser SET FailedLoginAttempts = 0 WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeactivateAccount(string username)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "UPDATE Advertiser SET isActive = 0 WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
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

        private bool IsApprovalPending(string username)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "SELECT Approval FROM Advertiser WHERE Username = @username AND Approval IS NOT NULL AND Approval <> 'Approved'";
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