using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace MoProf2303_25432
{
    public partial class sRateTutorPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTutors();
            }
        }

        private void LoadTutors()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = "SELECT Tutor_Id, FirstName + ' ' + LastName AS TutorName FROM Tutor";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlTutors.DataSource = reader;
                ddlTutors.DataTextField = "TutorName";
                ddlTutors.DataValueField = "Tutor_Id";
                ddlTutors.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string session = Convert.ToString(Session["Student_Id"]);
            int tutorId = int.Parse(ddlTutors.SelectedValue);
            int rating = int.Parse(txtRating.Text);
            string comments = txtComments.Text;
            
            int studentId = int.Parse(session);

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string checkQuery = "SELECT COUNT(*) FROM TutorRatings WHERE Student_Id = @StudentId AND Tutor_Id = @TutorId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@StudentId", studentId);
                checkCmd.Parameters.AddWithValue("@TutorId", tutorId);
                conn.Open();

                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    lblMessage.Text = "You have already recommended this tutor.";
                }
                else
                {
                    string insertQuery = "INSERT INTO TutorRatings (Student_Id, Tutor_Id, Rating, Comments) VALUES (@StudentId, @TutorId, @Rating, @Comments)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@StudentId", studentId);
                    insertCmd.Parameters.AddWithValue("@TutorId", tutorId);
                    insertCmd.Parameters.AddWithValue("@Rating", rating);
                    insertCmd.Parameters.AddWithValue("@Comments", comments);
                    insertCmd.ExecuteNonQuery();

                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Your rating has been submitted successfully.";
                }

                conn.Close();
            }
        }
    }
}
