using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class StudentMasterPage : System.Web.UI.MasterPage
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Student_Id"])))
                {
                    Response.Redirect("LogInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else if (!IsPostBack)
                {
                    if (Session["Student_Id"] != null)
                    {
                        string Student_Id = Session["Student_Id"].ToString();
                        LoadStudentInfo(Student_Id);

                    }
                }
            }
        }


        private void LoadStudentInfo(string Student_Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    string query = @"
                SELECT s.*, d.District_Name, vt.VT_NAME, g.Gender as g, e.Grade_Name as e
                FROM Student s
                JOIN District d ON s.District = d.District_Id
                JOIN Village_Town vt ON s.VillageTown = vt.VT_Id
                JOIN Gender g ON s.Gender = g.Gender_Id
                JOIN Education_Grade e ON s.EduGrade = e.Grade_Id
                WHERE s.Student_Id = @Student_Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Student_Id", Student_Id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows && dr.Read())
                            {
                                lblName.Text = $"{dr["FirstName"]} {dr["MiddleName"]} {dr["LastName"]}";
                                lblbio.Text = dr["Bio"].ToString();
                                lblgrade.Text = dr["e"].ToString();
                                lblGender.Text = dr["g"].ToString();
                                lblDOB.Text = DateTime.Parse(dr["DateOfBirth"].ToString()).ToString("dd-MMM-yyyy");
                                lblContact.Text = dr["MobileNumber"].ToString();
                                lblEmail.Text = dr["Email"].ToString();
                                lblAddress.Text = $"{dr["StrAddress"]}, {dr["VT_NAME"]}, {dr["District_Name"]}";
                                imgProfilePicture.ImageUrl = dr["ProfilePicture"] != DBNull.Value ? dr["ProfilePicture"].ToString() : "~/tImages/slider-02.jpg";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                // LogError(ex);
            }
        }


        void lgout()
        {
            if (Session["Student_Id"] != null || Session["Admin_Id"] != null)
            {
                //Remove all session
                Session.RemoveAll();
                //Destroy all Session objects
                Session.Abandon();
                //Redirect to homepage or login page
                Response.Redirect("~/homePage.aspx");
            }
        }

        protected void LogoutLinkButton_Click(object sender, EventArgs e)
        {
            lgout();
        }
    }
}