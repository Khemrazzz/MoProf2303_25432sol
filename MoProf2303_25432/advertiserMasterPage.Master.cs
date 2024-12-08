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
    public partial class advertiserMasterPage : System.Web.UI.MasterPage
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Advertiser_Id"])))
                {
                    Response.Redirect("adsLogInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else
                {
                    if (Session["Advertiser_Id"] != null)
                    {
                        string Advertiser_Id = Session["Advertiser_Id"].ToString();
                        LoadAdvertiserInfo(Advertiser_Id);
                    }
                }
            }
        }


        private void LoadAdvertiserInfo(string Advertiser_Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    string query = @"
                SELECT a.*, d.District_Name, vt.VT_Name
                FROM Advertiser a
                JOIN District d ON a.District = d.District_Id
                JOIN Village_Town vt ON a.VillageTown = vt.VT_Id
                WHERE a.Advertiser_Id = @Advertiser_Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Advertiser_Id", Advertiser_Id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows && dr.Read())
                            {
                                lblName.Text = $"{dr["CompanyName"]}";
                                lblwebsite.Text = $"{dr["Website"]}";
                                lbldate.Text = DateTime.Parse(dr["RegistrationDate"].ToString()).ToString("dd-MMM-yyyy");
                                lblContact.Text = dr["MobileNumber"].ToString();
                                lblEmail.Text = dr["Email"].ToString();
                                lblAddress.Text = $"{dr["StrAddress"]}, {dr["VT_Name"]}, {dr["District_Name"]}";
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
            if (Session["Advertiser_Id"] != null || Session["Admin_Id"] != null)
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