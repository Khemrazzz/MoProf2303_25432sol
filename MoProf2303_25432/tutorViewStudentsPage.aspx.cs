using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class tutorViewStudentsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPaymentsGrid();
            }
        }

        private void BindPaymentsGrid()
        {
            string connString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    string tutorId = Session["Tutor_Id"]?.ToString();
                    if (string.IsNullOrEmpty(tutorId))
                    {
                        // Handle the case when Tutor_Id is not available in the session
                        lblMessage.Text = "Tutor ID is missing.";
                        return;
                    }

                    string query = @"SELECT 
                                        B.Booking_Id,
                                        S.Student_Id,
                                        S.FirstName,
                                        S.LastName,
                                        S.Email,
                                        C.Course_Id,
                                        C.Subject_Name,
                                        B.Payment
                                    FROM 
                                        Booking B
                                    JOIN 
                                        Student S ON B.Student_Id = S.Student_Id
                                    JOIN 
                                        Courses C ON B.Course_Id = C.Course_Id
                                    WHERE 
                                        B.Payment = 'Process'
                                        AND B.Tutor_Id = @TutorId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TutorId", tutorId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvPayments.DataSource = dt;
                    gvPayments.DataBind();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                }
            }
        }

        protected void gvPayments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ApprovePayment")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPayments.Rows[index];
                int bookingId = Convert.ToInt32(row.Cells[0].Text);

                ApprovePayment(bookingId);
                BindPaymentsGrid();
            }
        }

        private void ApprovePayment(int bookingId)
        {
            string connString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    string query = "UPDATE Booking SET Payment = 'Approved' WHERE Booking_Id = @BookingId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BookingId", bookingId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Payment approved successfully.";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
