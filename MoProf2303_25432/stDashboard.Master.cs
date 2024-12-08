using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace MoProf2303_25432
{
    public partial class stDashboard : MasterPage
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Tutor_Id"])))
                {
                    Response.Redirect("LogInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else if (Session["Tutor_Id"] != null)
                {
                    string Tutor_Id = Session["Tutor_Id"].ToString();
                    LoadTutorInfo(Tutor_Id);
                }
            }
        }

        private void LoadTutorInfo(string Tutor_Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    string query = @"
                    SELECT t.*, d.District_Name, vt.VT_NAME, g.Gender as g
                    FROM Tutor t
                    JOIN District d ON t.District = d.District_Id
                    JOIN Village_Town vt ON t.VillageTown = vt.VT_Id
                    JOIN Gender g ON t.Gender = g.Gender_Id
                    WHERE t.Tutor_Id = @Tutor_Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Tutor_Id", Tutor_Id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows && dr.Read())
                            {
                                lblName.Text = $"{dr["FirstName"]} {dr["MiddleName"]} {dr["LastName"]}";
                                lblbio.Text = dr["Bio"].ToString();
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
            if (Session["Tutor_Id"] != null || Session["adminuname"] != null)
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
