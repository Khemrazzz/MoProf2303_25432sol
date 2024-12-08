using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class homePage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourses();
                LoadInstructors();
                BindTestimonials();
                BindReviews();
            }
        }

        protected void sendMessageButton_Click(object sender, EventArgs e)
        {
            string firstname = txtfname.Text;
            string lastname = txtlname.Text;
            string email = txtemail.Text;
            string subject = txtsubject.Text;
            string message = txtmessage.Text;

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string ContactUsPage = @"INSERT INTO [dbo].[ContactPage] (First_Name, Last_Name, Email, Subject, Message) 
                      VALUES (@First_Name, @Last_Name, @Email, @Subject, @Message)";

                using (SqlCommand cmd = new SqlCommand(ContactUsPage, conn))
                {
                    cmd.Parameters.AddWithValue("@First_Name", firstname);
                    cmd.Parameters.AddWithValue("@Last_Name", lastname);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Subject", subject);
                    cmd.Parameters.AddWithValue("@Message", message);

                    sendemail();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            // Optionally, display a success message or redirect to another page
            Response.Redirect("homePage.aspx");
        }

        private void sendemail()
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage m = new MailMessage(smtpSection.From, txtemail.Text.Trim()))
            {
                SmtpClient sc = new SmtpClient();
                try
                {
                    m.Subject = "MoProf-Support";
                    m.IsBodyHtml = true;
                    StringBuilder msgBody = new StringBuilder();
                    msgBody.Append("Dear " + txtfname.Text + ",Thank you contacting us, Our support team shall be in touch with you in 2-3 business days.");
                    m.Body = msgBody.ToString();
                    sc.Host = smtpSection.Network.Host;
                    sc.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    sc.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    sc.Credentials = networkCred;
                    sc.Port = smtpSection.Network.Port;
                    sc.Send(m);

                    Response.Write("Email Sent successfully");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        private void BindCourses()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
                    SELECT c.Course_Id, c.Subject_Name, c.CoursePicture, t.FirstName + ' ' + t.LastName AS Tutor_Name 
                    FROM Courses c
                    JOIN Tutor t ON c.Tutor_Id = t.Tutor_Id
                    ORDER BY c.Date DESC"; // Order by Date in descending order to get the most recent courses
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(rdr);
                    rptCourses.DataSource = dt;
                    rptCourses.DataBind();
                }
            }
        }

        private void LoadInstructors()
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
                    GROUP BY 
                        t.Tutor_Id, t.FirstName, t.LastName, t.ProfilePicture, t.Status";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    rptInstructors.DataSource = dt;
                    rptInstructors.DataBind();
                }
            }
        }

        private void BindReviews()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
                    SELECT 
                        s.FirstName + ' ' + s.LastName AS StudentName, 
                        t.FirstName + ' ' + t.LastName AS TutorName, 
                        r.Rating, 
                        r.Comments, 
                        r.RatingDate
                    FROM 
                        TutorRatings r
                    JOIN 
                        Student s ON r.Student_Id = s.Student_Id
                    JOIN 
                        Tutor t ON r.Tutor_Id = t.Tutor_Id
                    ORDER BY 
                        r.RatingDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    rptReviews.DataSource = dt;
                    rptReviews.DataBind();
                }
            }
        }

        private void BindTestimonials()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = @"
                    SELECT 
                        t.FirstName, 
                        t.LastName, 
                        t.Message, 
                        s.ProfilePicture
                    FROM 
                        Testimonial t
                    JOIN 
                        Student s ON t.Student_Id = s.Student_Id
                    ORDER BY 
                        t.submission_date DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptTestimonials.DataSource = dt;
                rptTestimonials.DataBind();
            }
        }
    }
}
