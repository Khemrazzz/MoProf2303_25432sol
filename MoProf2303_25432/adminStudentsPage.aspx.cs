using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class adminStudentsPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStudents();
            }
        }

        private void BindStudents(string searchTerm = "")
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
                    SELECT Student_Id, FirstName, LastName, Username, Email, IsActive
                    FROM Student
                    WHERE Username LIKE @SearchTerm AND Approval = 'approved'
                    ORDER BY Student_Id DESC
                ";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvStudents.DataSource = dt;
                gvStudents.DataBind();
            }
        }

        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToggleActive")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvStudents.Rows[index];
                int studentId = Convert.ToInt32(row.Cells[0].Text);
                bool isActive = ((Button)row.FindControl("btnToggleActive")).Text == "Freeze";

                using (SqlConnection conn = new SqlConnection(_conString))
                {
                    string query = "UPDATE Student SET IsActive = @IsActive WHERE Student_Id = @StudentId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IsActive", !isActive);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Refresh the grid
                BindStudents(txtSearch.Text.Trim());
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindStudents(txtSearch.Text.Trim());
        }
    }
}
