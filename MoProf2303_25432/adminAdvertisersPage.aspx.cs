using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class adminAdvertisersPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAdvertisers();
            }
        }

        private void BindAdvertisers(string searchTerm = "")
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
                    SELECT Advertiser_Id, CompanyName, Username, Email, IsActive, Approval
                    FROM Advertiser
                    WHERE Username LIKE @SearchTerm AND Approval = 'approved'
                    ORDER BY Advertiser_Id DESC
                ";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvAdvertisers.DataSource = dt;
                gvAdvertisers.DataBind();
            }
        }

        protected void gvAdvertisers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "ToggleActive")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvAdvertisers.Rows[index];
                int advertiserId = Convert.ToInt32(row.Cells[0].Text);
                bool isActive = ((Button)row.FindControl("btnToggleActive")).Text == "Freeze";

                using (SqlConnection conn = new SqlConnection(_conString))
                {
                    string query = "UPDATE Advertiser SET IsActive = @IsActive WHERE Advertiser_Id = @AdvertiserId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IsActive", !isActive);
                    cmd.Parameters.AddWithValue("@AdvertiserId", advertiserId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Refresh the grid
                BindAdvertisers(txtSearch.Text.Trim());
            }
        }

        

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindAdvertisers(txtSearch.Text.Trim());
        }
    }
}
