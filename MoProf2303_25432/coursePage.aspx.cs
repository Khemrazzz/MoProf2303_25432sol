using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class coursePage : System.Web.UI.Page
    {
        string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourses();
                PopulateCategoryList();
                PopulateDistrictList();
            }
        }

        protected void SearchCourses(object sender, EventArgs e)
        {
            BindCourses(txtKeyword.Text.Trim(), ddlCategory.SelectedValue, ddlDistrict.SelectedValue);
        }

        private void BindCourses(string keyword = "", string category = "", string district = "")
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
                    SELECT c.Course_Id, c.Subject_Name, c.CoursePicture, t.FirstName + ' ' + t.LastName AS Tutor_Name 
                    FROM Courses c
                    JOIN Tutor t ON c.Tutor_Id = t.Tutor_Id
                    WHERE 1=1";

                if (!string.IsNullOrEmpty(keyword))
                {
                    query += " AND c.Subject_Name LIKE @Keyword";
                }
                if (!string.IsNullOrEmpty(category))
                {
                    query += " AND c.Subject_Name IN (SELECT Subject_Name FROM Courses WHERE Category_Id = @Category)";
                }
                if (!string.IsNullOrEmpty(district))
                {
                    query += " AND c.District = @District";
                }

                query += " ORDER BY c.Date DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    }
                    if (!string.IsNullOrEmpty(category))
                    {
                        cmd.Parameters.AddWithValue("@Category", category);
                    }
                    if (!string.IsNullOrEmpty(district))
                    {
                        cmd.Parameters.AddWithValue("@District", district);
                    }

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(rdr);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
        }

        private void PopulateCategoryList()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = "SELECT Category_Id, Category_Name FROM Category";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    ddlCategory.DataSource = rdr;
                    ddlCategory.DataTextField = "Category_Name";
                    ddlCategory.DataValueField = "Category_Id";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
                }
            }
        }

        private void PopulateDistrictList()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = "SELECT District_Id, District_Name FROM District";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    ddlDistrict.DataSource = rdr;
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_Id"; // Changed this line to ensure correct data binding
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("Select District", ""));
                }
            }
        }
    }
}