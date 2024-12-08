using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class adminLogInPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
             
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtuname.Text.Trim();
            string password = txtpass.Text.Trim();

            if (IsValidUser(username, password))
            {
                // Redirect to another page or perform other actions upon successful login
                Response.Redirect("adminDashboardPage.aspx");
            }
            else
            {
                // Display error message if authentication fails
                lblmsg.Text = "Invalid Username or password.";
                lblmsg.Visible = true;
            }
        }

        private bool IsValidUser(string username, string password)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Admin WHERE Username=@Username AND Password=@Password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    return count == 1;
                }
            }
        }
    }
}