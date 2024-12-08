using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class instructorsPage : System.Web.UI.Page
    {
        private string _conString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInstructors();
            }
        }

        private void LoadInstructors(string searchTerm = "")
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
                    SELECT 
                        t.Tutor_Id,
                        t.FirstName, 
                        t.LastName, 
                        t.ProfilePicture, 
                        t.Status,
                        ISNULL(AVG(r.Rating), 0) AS AverageRating,
                        COUNT(r.Rating_Id) AS TotalReviews
                    FROM 
                        Tutor t
                    LEFT JOIN 
                        TutorRatings r ON t.Tutor_Id = r.Tutor_Id
                    WHERE 
                        (@searchTerm = '' OR t.FirstName LIKE '%' + @searchTerm + '%' OR t.LastName LIKE '%' + @searchTerm + '%')
                    GROUP BY 
                        t.Tutor_Id, t.FirstName, t.LastName, t.ProfilePicture, t.Status";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    rptInstructors.DataSource = dt;
                    rptInstructors.DataBind();
                }
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadInstructors(txtSearch.Text.Trim());
        }
    }
}