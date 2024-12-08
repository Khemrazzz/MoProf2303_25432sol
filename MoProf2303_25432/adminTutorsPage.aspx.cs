using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class adminTutorsPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTutors();
            }
        }

        private void BindTutors(string searchTerm = "")
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
                    SELECT Tutor_Id, FirstName, LastName, Username, Email, IsActive
                    FROM Tutor
                    WHERE Username LIKE @SearchTerm AND Approval = 'approved'
                    ORDER BY Tutor_Id DESC
                ";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvTutors.DataSource = dt;
                gvTutors.DataBind();
            }
        }

        protected void gvTutors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToggleActive")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvTutors.Rows[index];
                int tutorId = Convert.ToInt32(row.Cells[0].Text);
                bool isActive = ((Button)row.FindControl("btnToggleActive")).Text == "Freeze";

                using (SqlConnection conn = new SqlConnection(_conString))
                {
                    string query = "UPDATE Tutor SET IsActive = @IsActive WHERE Tutor_Id = @TutorId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IsActive", !isActive);
                    cmd.Parameters.AddWithValue("@TutorId", tutorId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Refresh the grid
                BindTutors(txtSearch.Text.Trim());
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindTutors(txtSearch.Text.Trim());
        }
    }
}
