using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class adsEditProfilePage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                PopulateDistrictList2(ddldistrict);

                if (string.IsNullOrEmpty(Convert.ToString(Session["Advertiser_Id"])))
                {
                    Response.Redirect("adsLogInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else
                {
                    if (Session["Advertiser_Id"] != null)
                    {
                        LoadAdvertiserData();
                    }
                }
            }
        }

        private void LoadAdvertiserData()
        {
            string Advertiser_Id = Session["Advertiser_Id"].ToString();
            using (SqlConnection connection = new SqlConnection(_conString))
            {
                connection.Open();
                string query = "SELECT CompanyName, Email, MobileNumber, StrAddress, District, VillageTown, Website, ProfilePicture FROM Advertiser WHERE Advertiser_Id = @Advertiser_Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Advertiser_Id", Advertiser_Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtcname.Text = reader["CompanyName"].ToString();
                            txtemail.Text = reader["Email"].ToString();
                            txturl.Text = reader["Website"].ToString();
                            txtmob.Text = reader["MobileNumber"].ToString();
                            txtstreetaddress.Text = reader["StrAddress"].ToString();
                            ddldistrict.SelectedValue = reader["District"].ToString();
                            PopulateVillageTownList2(ddlvt, Convert.ToInt32(reader["District"]));
                            ddlvt.SelectedValue = reader["VillageTown"].ToString();
                           
                            if (reader["ProfilePicture"] != DBNull.Value)
                            {
                                string profilePicUrl = reader["ProfilePicture"].ToString();
                                imgProfilePicture.ImageUrl = profilePicUrl;
                            }
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Advertiser_Id = Session["Advertiser_Id"].ToString();
            string companyName = txtcname.Text.Trim();
            string email = txtemail.Text.Trim();
            string mobileNumber = txtmob.Text.Trim();
            string street = txtstreetaddress.Text.Trim();
            string district = ddldistrict.SelectedValue;
            string vilagetown = ddlvt.SelectedValue;
            string website = txturl.Text.Trim();
            string currentProfilePic = imgProfilePicture.ImageUrl;
            using (SqlConnection connection = new SqlConnection(_conString))
            {
                connection.Open();
                string query = "UPDATE Advertiser SET CompanyName = @CompanyName, Website = @Website, Email = @Email, MobileNumber = @MobileNumber, StrAddress = @StrAddress, VillageTown = @VillageTown, District = @District, ProfilePicture = @ProfilePicture WHERE Advertiser_Id = @Advertiser_Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CompanyName", companyName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Website", website);
                    command.Parameters.AddWithValue("@MobileNumber", mobileNumber);
                    command.Parameters.AddWithValue("@StrAddress", street);
                    command.Parameters.AddWithValue("@District", district);
                    command.Parameters.AddWithValue("@VillageTown", vilagetown);
                    command.Parameters.AddWithValue("@Advertiser_Id", Advertiser_Id);

                    if (fuppicture5.HasFile)
                    {
                        string fileName = Path.GetFileName(fuppicture5.PostedFile.FileName);
                        string filePath = Server.MapPath("~/adsImages/" + fileName);
                        fuppicture5.SaveAs(filePath);
                        command.Parameters.AddWithValue("@ProfilePicture", "~/adsImages/" + fileName);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ProfilePicture", currentProfilePic);
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    lblMessage.Text = rowsAffected > 0 ? "Profile updated successfully." : "Error updating profile.";
                    lblMessage.ForeColor = rowsAffected > 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                }
            }
        }

        

        private void PopulateDistrictList2(DropDownList dropdown)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("GetAllDistricts", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "District_Name";
                dropdown.DataValueField = "District_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Select district", "-1"));
            }
        }

        private void PopulateVillageTownList2(DropDownList dropdown, int districtId)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("GetVillagesTownsByDistrict", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                scmd.Parameters.AddWithValue("@DistrictId", districtId);
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "VT_Name";
                dropdown.DataValueField = "VT_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Select village/town", "-1"));
            }
        }

        protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddldistrict.SelectedValue, out int districtId) && districtId != -1)
            {
                PopulateVillageTownList2(ddlvt, districtId);
            }
            else
            {
                ddlvt.Items.Clear();
                ddlvt.Items.Insert(0, new ListItem("Select village/town", "-1"));
            }
        }
    }
}
