using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class studentpaymentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string courseId = Request.QueryString["Course_Id"];
                if (!string.IsNullOrEmpty(courseId))
                {
                    LoadCourseDetails(courseId);
                }
                else
                {
                    Response.Redirect("studentPayStsPage.aspx");
                }
            }
        }

        private void LoadCourseDetails(string courseId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT c.Subject_Name AS CourseName, 
                                        t.FirstName + ' ' + t.LastName AS TutorName, 
                                        c.Fees 
                                 FROM Courses c
                                 JOIN Tutor t ON c.Tutor_Id = t.Tutor_Id
                                 WHERE c.Course_Id = @Course_Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Course_Id", courseId);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        lblCourseName.Text = "Course: " + dr["CourseName"].ToString();
                        lblTutorName.Text = "Tutor: " + dr["TutorName"].ToString();
                        lblFees.Text = "Fees: " + Convert.ToDecimal(dr["Fees"]).ToString("C");
                    }
                    conn.Close();
                }
            }
        }

        protected void btnSubmitPayment_Click(object sender, EventArgs e)
        {
            if (Session["Student_Id"] != null)
            {
                int studentId = Convert.ToInt32(Session["Student_Id"]);
                string courseId = Request.QueryString["Course_Id"];
                string paymentStatus = "Process";
                string cvv = txtCVV.Text;
                string cardNumber = txtCardNumber.Text;
                string expireDate = txtExpirationDate.Text;

                // Fetch Tutor_Id based on Course_Id
                string tutorId = GetTutorId(courseId);

                string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Booking (Tutor_Id, Student_Id, Course_Id, Payment, CVV, CardNumber, ExpireDate)
                                     VALUES (@Tutor_Id, @Student_Id, @Course_Id, @Payment, @CVV, @CardNumber, @ExpireDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Tutor_Id", tutorId);
                        cmd.Parameters.AddWithValue("@Student_Id", studentId);
                        cmd.Parameters.AddWithValue("@Course_Id", courseId);

                        cmd.Parameters.AddWithValue("@Payment", paymentStatus);
                        cmd.Parameters.AddWithValue("@CVV", cvv);
                        cmd.Parameters.AddWithValue("@CardNumber", cardNumber);
                        cmd.Parameters.AddWithValue("@ExpireDate", expireDate);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                // For demonstration purposes, let's assume the payment is successful
                Response.Write("<script>alert('Payment successful!');</script>");
                Response.Redirect("studentPayStsPage.aspx");
            }
            else
            {
                // Handle the case where Student_Id is not in the session
                Response.Redirect("LogInPage.aspx");
            }
        }

        private string GetTutorId(string courseId)
        {
            string tutorId = null;
            string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT Tutor_Id FROM Courses WHERE Course_Id = @Course_Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Course_Id", courseId);
                    conn.Open();
                    tutorId = cmd.ExecuteScalar().ToString();
                    conn.Close();
                }
            }
            return tutorId;
        }
    }
}