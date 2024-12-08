using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace MoProf2303_25432
{
    public partial class testimonialPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            BindTestimonials();

        }

        private void BindTestimonials()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
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