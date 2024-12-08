using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.IO;
using System.Text;

namespace MoProf2303_25432
{
    public partial class tutorStudentsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStudents();
            }
        }

        private void BindStudents()
        {
            if (Session["Tutor_Id"] != null)
            {
                string tutorId = Session["Tutor_Id"].ToString();
                string connStr = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT s.Student_Id, s.FirstName, s.LastName, s.Email, s.MobileNumber, s.EduStatus, eg.Grade_Name, s.CertificatePicture, b.Payment, b.Booking_Id " +
                                   "FROM Booking b " +
                                   "INNER JOIN Student s ON b.Student_Id = s.Student_Id " +
                                   "INNER JOIN Education_Grade eg ON s.EduGrade = eg.Grade_Id " +
                                   "WHERE b.Payment IS NULL AND b.Tutor_Id = @TutorId";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@TutorId", tutorId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptStudents.DataSource = dt;
                    rptStudents.DataBind();
                }
            }
        }

        protected void rptStudents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Accept")
            {
                UpdatePaymentStatus(e.CommandArgument.ToString(), "Pending", "Your booking has been accepted.");
            }
            else if (e.CommandName == "Reject")
            {
                UpdatePaymentStatus(e.CommandArgument.ToString(), "Rejected", "Your booking has been rejected.");
            }
            else if (e.CommandName == "ViewCertificate")
            {
                string certificatePath = e.CommandArgument.ToString();
                ShowCertificate(certificatePath);
            }
        }

        private void UpdatePaymentStatus(string bookingId, string status, string emailBody)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Booking SET Payment = @Payment WHERE Booking_Id = @BookingId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Payment", status);
                    cmd.Parameters.AddWithValue("@BookingId", bookingId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Get student email and certificate path
                string email = "";
                string certificatePath = "";
                string studentName = "";
                query = "SELECT s.Email, s.CertificatePicture, s.FirstName, s.LastName FROM Booking b INNER JOIN Student s ON b.Student_Id = s.Student_Id WHERE b.Booking_Id = @BookingId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookingId", bookingId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            email = reader["Email"].ToString();
                            certificatePath = reader["CertificatePicture"].ToString();
                            studentName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                        }
                    }
                    conn.Close();
                }

                // Send email
                if (!string.IsNullOrEmpty(email))
                {
                    string subject = "Booking Status Update";
                    string body = $"Dear {studentName},<br/><br/>{emailBody}<br/><br/>Thank you.";
                    SendEmail(email, subject, body, certificatePath);
                }
            }
            BindStudents();
        }

        private void ShowCertificate(string certificatePath)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showCertificate", $"showResultImage('{certificatePath}');", true);
        }

        private void SendEmail(string toEmail, string subject, string body, string attachmentPath)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage m = new MailMessage(smtpSection.From, toEmail))
            {
                SmtpClient sc = new SmtpClient();
                try
                {
                    m.Subject = subject;
                    m.IsBodyHtml = true;
                    m.Body = body;
                    if (!string.IsNullOrEmpty(attachmentPath))
                    {
                        m.Attachments.Add(new Attachment(Server.MapPath(attachmentPath)));
                    }
                    sc.Host = smtpSection.Network.Host;
                    sc.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    sc.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    sc.Credentials = networkCred;
                    sc.Port = smtpSection.Network.Port;
                    sc.Send(m);
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                }
            }
        }
    }
}