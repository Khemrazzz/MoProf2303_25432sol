using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class studentTestimonials : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Student_Id"] == null)
            {
                Session["ReturnUrl"] = Request.Url.AbsoluteUri; // Store the current URL
                Response.Redirect("LogInPage.aspx"); // Redirect to login page if not logged in
            }

            if (!IsPostBack)
            {
                LoadStudentDetails();
            }
        }

        protected void LoadStudentDetails()
        {
            int Student_Id = Convert.ToInt32(Session["Student_Id"]);
            string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
            string query = "SELECT FirstName, LastName, Email FROM [dbo].[Student] WHERE Student_Id = @Student_Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Student_Id", Student_Id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtfName.Text = reader["FirstName"].ToString();
                        Textlname.Text = reader["LastName"].ToString();
                        txtemail.Text = reader["Email"].ToString();
                    }

                    reader.Close();
                }
            }
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string firstName = txtfName.Text;
                string lastName = Textlname.Text;
                string email = txtemail.Text;
                string message = txtmessage.Text;

                int Student_Id = Convert.ToInt32(Session["Student_Id"]);

                string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO [dbo].[Testimonial] (Student_Id, FirstName, LastName, Email, Message, submission_date) VALUES (@Student_Id, @FirstName, @LastName, @Email, @Message, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Student_Id", Student_Id);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Message", message);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                // Optionally, display a success message or redirect to another page
                Response.Redirect("homepage.aspx");
            }
        }
    }
}