using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class adminAuditPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLoginAttempts();
            }
        }

        private void BindLoginAttempts(string searchTerm = "")
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
            SELECT la.Username, la.AttemptTime, la.IPAddress, la.LoginType, la.IsSuccessful,
                   (CASE WHEN t.failedLoginAttempts IS NOT NULL THEN 'Tutor: ' + CAST(t.failedLoginAttempts AS NVARCHAR) ELSE '' END +
                    CASE WHEN s.failedLoginAttempts IS NOT NULL THEN ' Student: ' + CAST(s.failedLoginAttempts AS NVARCHAR) ELSE '' END +
                    CASE WHEN a.failedLoginAttempts IS NOT NULL THEN ' Advertiser: ' + CAST(a.failedLoginAttempts AS NVARCHAR) ELSE '' END) AS failedLoginAttempts
            FROM LoginAudit la
            LEFT JOIN Tutor t ON la.Username = t.Username
            LEFT JOIN Student s ON la.Username = s.Username
            LEFT JOIN Advertiser a ON la.Username = a.Username
            WHERE la.Username LIKE @SearchTerm
            ORDER BY la.AttemptTime DESC
        ";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLoginAttempts.DataSource = dt;
                gvLoginAttempts.DataBind();
            }
        }

        protected void gvLoginAttempts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Unblock")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvLoginAttempts.Rows[index];
                string username = row.Cells[1].Text;

                using (SqlConnection conn = new SqlConnection(_conString))
                {
                    string query = @"
                        UPDATE Tutor SET failedLoginAttempts = 0, IsActive = 1 WHERE Username = @Username;
                        UPDATE Student SET failedLoginAttempts = 0, IsActive = 1 WHERE Username = @Username;
                        UPDATE Advertiser SET failedLoginAttempts = 0, IsActive = 1 WHERE Username = @Username;
                    ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Refresh the grid
                BindLoginAttempts(txtSearch.Text.Trim());
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindLoginAttempts(txtSearch.Text.Trim());
        }
    }
}