using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

namespace MoProf2303_25432
{
    public partial class courseDetailPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["CourseID"] != null)
                {
                    int courseId = int.Parse(Request.QueryString["CourseID"]);
                    BindCourseDetails(courseId);
                }
            }
        }

        private void BindCourseDetails(int courseId)
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
                    SELECT c.Subject_Name, c.CoursePicture, c.Description, c.Fees, 
                           t.FirstName + ' ' + t.LastName AS Tutor_Name, c.Date, c.DayName, 
                           c.TimeSlot, c.ModeName, c.District, c.VillageTown, c.StrAddress, 
                           c.SeatCount 
                    FROM Courses c
                    JOIN Tutor t ON c.Tutor_Id = t.Tutor_Id
                    WHERE c.Course_Id = @CourseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        courseTitle.InnerText = rdr["Subject_Name"].ToString();
                        courseImage.Src = rdr["CoursePicture"].ToString();
                        courseDescription.InnerText = rdr["Description"].ToString();
                        courseInstructor.InnerText = rdr["Tutor_Name"].ToString();

                        courseLectures.InnerText = "15"; // Assuming a static number of lectures for now
                        courseDuration.InnerText = "10.00 Hrs"; // Assuming a static duration for now
                        courseSkillLevel.InnerText = "All Level"; // Assuming a static skill level for now
                        courseLanguage.InnerText = "English"; // Assuming a static language for now
                        coursePrice.InnerText = "Course Price: $" + rdr["Fees"].ToString();
                    }
                }
            }
        }

        protected void btnEnroll_Click(object sender, EventArgs e)
        {
            if (Session["Student_Id"] == null)
            {
                string currentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                Response.Redirect("~/logInPage.aspx?ReturnUrl=" + Server.UrlEncode(currentUrl));
                return;
            }

            int studentId = int.Parse(Session["Student_Id"].ToString());
            int courseId = int.Parse(Request.QueryString["CourseID"]);
            int tutorId = GetTutorIdByCourseId(courseId);

            if (IsAlreadyEnrolled(studentId, courseId))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You have already enrolled in this course.');", true);
                return;
            }

            EnrollStudentInCourse(studentId, tutorId, courseId);
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your request has been sent.');", true);
        }

        private int GetTutorIdByCourseId(int courseId)
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = "SELECT Tutor_Id FROM Courses WHERE Course_Id = @CourseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    conn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private bool IsAlreadyEnrolled(int studentId, int courseId)
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = "SELECT COUNT(*) FROM Booking WHERE Student_Id = @StudentId AND Course_Id = @CourseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void EnrollStudentInCourse(int studentId, int tutorId, int courseId)
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = "INSERT INTO Booking (Tutor_Id, Student_Id, Course_Id) VALUES (@TutorId, @StudentId, @CourseID)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TutorId", tutorId);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}