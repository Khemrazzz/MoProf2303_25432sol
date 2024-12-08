using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class adminInboxPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPendingApprovals();
            }
        }

        private void LoadPendingApprovals()
        {
            string query = @"
                SELECT Username, Email, 'Tutor' AS UserType, Tutor_Id AS User_Id 
                FROM Tutor 
                WHERE Approval = 'pending'
                UNION
                SELECT Username, Email, 'Student' AS UserType, Student_Id AS User_Id 
                FROM Student 
                WHERE Approval = 'pending'
                UNION
                SELECT Username, Email, 'Advertiser' AS UserType, Advertiser_Id AS User_Id 
                FROM Advertiser 
                WHERE Approval = 'pending'";

            using (SqlConnection con = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        rptApprovals.DataSource = dr;
                        rptApprovals.DataBind();
                    }
                }
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');
            int userId = Convert.ToInt32(args[0]);
            string userType = args[1];

            string query = $"UPDATE {userType} SET Approval = 'approved' WHERE {userType}_Id = @UserId";

            using (SqlConnection con = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadPendingApprovals();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');
            int userId = Convert.ToInt32(args[0]);
            string userType = args[1];

            string query = $"DELETE FROM {userType} WHERE {userType}_Id = @UserId";

            using (SqlConnection con = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadPendingApprovals();
        }
    }
}
