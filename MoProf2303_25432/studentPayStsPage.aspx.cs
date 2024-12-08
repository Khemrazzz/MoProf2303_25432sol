using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class studentPayStsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPendingPayments();
            }
        }

        private void BindPendingPayments()
        {
            if (Session["Student_Id"] != null)
            {
                int studentId = Convert.ToInt32(Session["Student_Id"]);
                string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT c.Course_Id, c.Subject_Name AS CourseName, 
                                     t.FirstName + ' ' + t.LastName AS TutorName, 
                                     c.Fees 
                              FROM Booking b
                              JOIN Courses c ON b.Course_Id = c.Course_Id
                              JOIN Tutor t ON b.Tutor_Id = t.Tutor_Id
                              WHERE b.Student_Id = @StudentId AND b.Payment = 'pending'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            gvPendingPayments.DataSource = dt;
                            gvPendingPayments.DataBind();
                        }
                    }
                }
            }
            else
            {
                // Handle the case where Student_Id is not in the session
                Response.Redirect("LogInPage.aspx");
            }
        }

        protected void gvPendingPayments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PayNow")
            {
                string courseId = e.CommandArgument.ToString();

                // Debugging line to check the command argument value
                // System.Diagnostics.Debug.WriteLine("Course_Id: " + courseId);

                // Redirect to the payment form with courseId
                Response.Redirect($"studentpaymentPage.aspx?Course_Id={courseId}");
            }
        }
    }
}