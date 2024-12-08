using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class adminEditCatPage : System.Web.UI.Page
    {
        private readonly string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM category", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvEditSubjects.DataSource = dt;
                gvEditSubjects.DataBind();
            }
        }

        private void LoadGrades()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Grade", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvEditGrades.DataSource = dt;
                gvEditGrades.DataBind();
            }
        }

        private void LoadCities()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM City", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvEditCities.DataSource = dt;
                gvEditCities.DataBind();
            }
        }

        private void LoadDistricts()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM District", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvEditDistricts.DataSource = dt;
                gvEditDistricts.DataBind();
            }
        }

        protected void btnEditSubjects_Click(object sender, EventArgs e)
        {
            pnlEditSubjects.Visible = true;
            pnlEditGrades.Visible = false;
            pnlEditCities.Visible = false;
            pnlEditDistricts.Visible = false;
            LoadCategories();
        }

        protected void btnEditGrades_Click(object sender, EventArgs e)
        {
            pnlEditSubjects.Visible = false;
            pnlEditGrades.Visible = true;
            pnlEditCities.Visible = false;
            pnlEditDistricts.Visible = false;
            LoadGrades();
        }

        protected void btnEditCities_Click(object sender, EventArgs e)
        {
            pnlEditSubjects.Visible = false;
            pnlEditGrades.Visible = false;
            pnlEditCities.Visible = true;
            pnlEditDistricts.Visible = false;
            LoadCities();
        }

        protected void btnEditDistricts_Click(object sender, EventArgs e)
        {
            pnlEditSubjects.Visible = false;
            pnlEditGrades.Visible = false;
            pnlEditCities.Visible = false;
            pnlEditDistricts.Visible = true;
            LoadDistricts();
        }

        protected void btnSaveSubject_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                string updateQuery = "UPDATE category SET Category_Name = @category_name";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                if (fuEditSubjectImage.HasFile)
                {
                    string fileName = Path.GetFileName(fuEditSubjectImage.PostedFile.FileName);
                    string filePath = "images/" + fileName;
                    fuEditSubjectImage.PostedFile.SaveAs(Server.MapPath(filePath));
                    updateQuery += ", subject_image = @subject_image";
                    cmd.Parameters.AddWithValue("@subject_image", filePath);
                }

                updateQuery += " WHERE Category_Id = @category_id";

                cmd.CommandText = updateQuery;
                cmd.Parameters.AddWithValue("@category_id", ViewState["CategoryId"]);
                cmd.Parameters.AddWithValue("@category_name", txtEditSubjectName.Text.Trim());

                cmd.ExecuteNonQuery();
                lblMsg.Text = "Category updated successfully!";
                lblMsg.Style["display"] = "block";
                LoadCategories();

                // Revert to default state
                txtEditSubjectName.Text = string.Empty;
                fuEditSubjectImage.Attributes.Clear();
            }
        }

        protected void btnCancelSubject_Click(object sender, EventArgs e)
        {
            txtEditSubjectName.Text = string.Empty;
            fuEditSubjectImage.Attributes.Clear();
            btnEditSubjects_Click(sender, e);
        }

        protected void gvEditSubjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int categoryId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditSubject")
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM category WHERE Category_Id = @CategoryId", con);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtEditSubjectName.Text = dr["Category_Name"].ToString();
                        ViewState["CategoryId"] = categoryId;
                    }
                }
            }
            else if (e.CommandName == "RemoveSubject")
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM category WHERE Category_Id = @CategoryId", con);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.ExecuteNonQuery();
                    lblMsg.Text = "Category removed successfully!";
                    lblMsg.Style["display"] = "block";
                    LoadCategories();
                }
            }
        }

        protected void gvEditSubjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEditSubjects.PageIndex = e.NewPageIndex;
            LoadCategories();
        }

        protected void btnNextSubject_Click(object sender, EventArgs e)
        {
            int newIndex = gvEditSubjects.PageIndex + 1;
            if (newIndex < gvEditSubjects.PageCount)
            {
                gvEditSubjects.PageIndex = newIndex;
                LoadCategories();
            }
        }

        protected void btnPreviousSubject_Click(object sender, EventArgs e)
        {
            int newIndex = gvEditSubjects.PageIndex - 1;
            if (newIndex >= 0)
            {
                gvEditSubjects.PageIndex = newIndex;
                LoadCategories();
            }
        }

        // Similar event handlers for Grades, Cities, and Districts

        protected void btnSaveGrade_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Grade SET Grade_Name = @Grade_Name WHERE Grade_Id = @Grade_Id", con);
                cmd.Parameters.AddWithValue("@Grade_Id", ViewState["GradeId"]);
                cmd.Parameters.AddWithValue("@Grade_Name", txtEditGradeName.Text.Trim());
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Grade updated successfully!";
                lblMsg.Style["display"] = "block";
                LoadGrades();

                // Revert to default state
                txtEditGradeName.Text = string.Empty;
            }
        }

        protected void btnCancelGrade_Click(object sender, EventArgs e)
        {
            txtEditGradeName.Text = string.Empty;
            btnEditGrades_Click(sender, e);
        }

        protected void gvEditGrades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int gradeId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditGrade")
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Grade WHERE Grade_Id = @Grade_Id", con);
                    cmd.Parameters.AddWithValue("@Grade_Id", gradeId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtEditGradeName.Text = dr["Grade_Name"].ToString();
                        ViewState["GradeId"] = gradeId;
                    }
                }
            }
            else if (e.CommandName == "RemoveGrade")
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Grade WHERE Grade_Id = @Grade_Id", con);
                    cmd.Parameters.AddWithValue("@Grade_Id", gradeId);
                    cmd.ExecuteNonQuery();
                    lblMsg.Text = "Grade removed successfully!";
                    lblMsg.Style["display"] = "block";
                    LoadGrades();
                }
            }
        }

        protected void gvEditGrades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEditGrades.PageIndex = e.NewPageIndex;
            LoadGrades();
        }

        protected void btnNextGrade_Click(object sender, EventArgs e)
        {
            int newIndex = gvEditGrades.PageIndex + 1;
            if (newIndex < gvEditGrades.PageCount)
            {
                gvEditGrades.PageIndex = newIndex;
                LoadGrades();
            }
        }

        protected void btnPreviousGrade_Click(object sender, EventArgs e)
        {
            int newIndex = gvEditGrades.PageIndex - 1;
            if (newIndex >= 0)
            {
                gvEditGrades.PageIndex = newIndex;
                LoadGrades();
            }
        }

        protected void btnSaveCity_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE City SET CityName = @CityName WHERE CityID = @CityID", con);
                cmd.Parameters.AddWithValue("@CityID", ViewState["CityID"]);
                cmd.Parameters.AddWithValue("@CityName", txtEditCityName.Text.Trim());
                cmd.ExecuteNonQuery();
                lblMsg.Text = "City updated successfully!";
                lblMsg.Style["display"] = "block";
                LoadCities();

                // Revert to default state
                txtEditCityName.Text = string.Empty;
            }
        }

        protected void btnCancelCity_Click(object sender, EventArgs e)
        {
            txtEditCityName.Text = string.Empty;
            btnEditCities_Click(sender, e);
        }

        protected void gvEditCities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int cityId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditCity")
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM City WHERE CityID = @CityID", con);
                    cmd.Parameters.AddWithValue("@CityID", cityId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtEditCityName.Text = dr["CityName"].ToString();
                        ViewState["CityID"] = cityId;
                    }
                }
            }
            else if (e.CommandName == "RemoveCity")
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM City WHERE CityID = @CityID", con);
                    cmd.Parameters.AddWithValue("@CityID", cityId);
                    cmd.ExecuteNonQuery();
                    lblMsg.Text = "City removed successfully!";
                    lblMsg.Style["display"] = "block";
                    LoadCities();
                }
            }
        }

        protected void gvEditCities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEditCities.PageIndex = e.NewPageIndex;
            LoadCities();
        }

        protected void btnNextCity_Click(object sender, EventArgs e)
        {
            int newIndex = gvEditCities.PageIndex + 1;
            if (newIndex < gvEditCities.PageCount)
            {
                gvEditCities.PageIndex = newIndex;
                LoadCities();
            }
        }

        protected void btnPreviousCity_Click(object sender, EventArgs e)
        {
            int newIndex = gvEditCities.PageIndex - 1;
            if (newIndex >= 0)
            {
                gvEditCities.PageIndex = newIndex;
                LoadCities();
            }
        }

        protected void btnSaveDistrict_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE District SET District_Name = @District_Name WHERE District_Id = @District_Id", con);
                cmd.Parameters.AddWithValue("@District_Id", ViewState["DistrictId"]);
                cmd.Parameters.AddWithValue("@District_Name", txtEditDistrictName.Text.Trim());
                cmd.ExecuteNonQuery();
                lblMsg.Text = "District updated successfully!";
                lblMsg.Style["display"] = "block";
                LoadDistricts();

                // Revert to default state
                txtEditDistrictName.Text = string.Empty;
            }
        }

        protected void btnCancelDistrict_Click(object sender, EventArgs e)
        {
            txtEditDistrictName.Text = string.Empty;
            btnEditDistricts_Click(sender, e);
        }

        protected void gvEditDistricts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int districtId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditDistrict")
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM District WHERE District_Id = @District_Id", con);
                    cmd.Parameters.AddWithValue("@District_Id", districtId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtEditDistrictName.Text = dr["District_Name"].ToString();
                        ViewState["DistrictId"] = districtId;
                    }
                }
            }
            else if (e.CommandName == "RemoveDistrict")
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM District WHERE District_Id = @District_Id", con);
                    cmd.Parameters.AddWithValue("@District_Id", districtId);
                    cmd.ExecuteNonQuery();
                    lblMsg.Text = "District removed successfully!";
                    lblMsg.Style["display"] = "block";
                    LoadDistricts();
                }
            }
        }

        protected void gvEditDistricts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEditDistricts.PageIndex = e.NewPageIndex;
            LoadDistricts();
        }

        protected void btnNextDistrict_Click(object sender, EventArgs e)
        {
            int newIndex = gvEditDistricts.PageIndex + 1;
            if (newIndex < gvEditDistricts.PageCount)
            {
                gvEditDistricts.PageIndex = newIndex;
                LoadDistricts();
            }
        }

        protected void btnPreviousDistrict_Click(object sender, EventArgs e)
        {
            int newIndex = gvEditDistricts.PageIndex - 1;
            if (newIndex >= 0)
            {
                gvEditDistricts.PageIndex = newIndex;
                LoadDistricts();
            }
        }
    }
}
