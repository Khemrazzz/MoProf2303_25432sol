using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class tutorCoursesPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Tutor_Id"])))
                {
                    Response.Redirect("logInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else
                {
                    LoadCourses();
                }
            }
        }

        private void LoadCourses()
        {
            var Tutor_Id = Session["Tutor_Id"];
            if (Tutor_Id != null)
            {
                using (SqlConnection conn = new SqlConnection(_conString))
                {
                    string query = @"
                    SELECT c.Course_Id, c.Subject_Name, c.Description, c.Fees, c.Date, d.DayName, t.TimeSlot, c.ModeName, 
       c.StrAddress, vt.VT_Name AS VillageTown, di.District_Name AS District, c.SeatCount, c.CoursePicture, 
       cat.Category_Name AS Category
FROM Courses c
LEFT JOIN Days d ON c.DayName = d.Day_Id
LEFT JOIN TimeSlot t ON c.TimeSlot = t.TimeSlot_ID
LEFT JOIN Village_Town vt ON c.VillageTown = vt.VT_Id
LEFT JOIN District di ON c.District = di.District_Id
LEFT JOIN Category cat ON c.Category_Id = cat.Category_Id
WHERE c.Tutor_Id = @Tutor_Id";


                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Tutor_Id", Tutor_Id.ToString());
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        rptCourses.DataSource = reader;
                        rptCourses.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "No courses found.";
                    }
                    reader.Close();
                }
            }
            else
            {
                Response.Redirect("logInPage.aspx");
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btnEdit = (Button)sender;
            string courseId = btnEdit.CommandArgument;
            Response.Redirect($"tutorEditCoursePage.aspx?CourseID={courseId}");
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Button btnRemove = (Button)sender;
            string Course_Id = btnRemove.CommandArgument;
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = "DELETE FROM Courses WHERE Course_Id = @Course_Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Course_Id", Course_Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadCourses();
        }
    }
}
