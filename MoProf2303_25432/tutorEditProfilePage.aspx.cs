using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class tutorEditProfilePage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCountry2();
                LoadGender2();
                PopulateDistrictList2(ddldistrict2);

                rvdob2.MinimumValue = DateTime.Now.AddYears(-65).ToShortDateString();
                rvdob2.MaximumValue = DateTime.Now.AddYears(-10).ToShortDateString();
                rvdob2.Type = ValidationDataType.Date;

                if (string.IsNullOrEmpty(Convert.ToString(Session["Tutor_Id"])))
                {
                    Response.Redirect("LogInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else if (!IsPostBack)
                {
                    if (Session["Tutor_Id"] != null)
                    {
                        LoadTutorData();
                    }
                }
            }
        }

        private void LoadTutorData()
        {
            string Tutor_Id = Session["Tutor_Id"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FirstName, MiddleName, LastName, Bio, Email, MobileNumber, StrAddress, District, VillageTown, Country, DateOfBirth, Gender, Status, ProfilePicture FROM Tutor WHERE Tutor_Id = @Tutor_Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tutor_Id", Tutor_Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtfname2.Text = reader["FirstName"].ToString();
                            txtmname2.Text = reader["MiddleName"].ToString();
                            txtlname2.Text = reader["LastName"].ToString();
                            txtbio2.Text = reader["Bio"].ToString();
                            txtemail2.Text = reader["Email"].ToString();
                            txtmob2.Text = reader["MobileNumber"].ToString();
                            txtstreetaddress2.Text = reader["StrAddress"].ToString();
                            ddldistrict2.SelectedValue = reader["District"].ToString();
                            PopulateVillageTownList2(ddlvt2, Convert.ToInt32(reader["District"]));
                            ddlvt2.SelectedValue = reader["VillageTown"].ToString();
                            txtdob2.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                            ddlgender2.SelectedValue = reader["Gender"].ToString();
                            ddlstatus.SelectedValue = reader["Status"].ToString();
                            ddlcountry2.SelectedValue = reader["Country"].ToString();
                            if (reader["ProfilePicture"] != DBNull.Value)
                            {
                                string profilePicUrl = reader["ProfilePicture"].ToString();
                                fuppicture3.Value = profilePicUrl;
                                imgProfilePicture.ImageUrl = profilePicUrl;
                            }
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Tutor_Id = Session["Tutor_Id"].ToString();
            string firstName = txtfname2.Text.Trim();
            string middleName = txtmname2.Text.Trim();
            string lastName = txtlname2.Text.Trim();
            string bio = txtbio2.Text.Trim();
            string email = txtemail2.Text.Trim();
            string mobileNumber = txtmob2.Text.Trim();
            string street = txtstreetaddress2.Text.Trim();
            string district = ddldistrict2.SelectedValue;
            string vilagetown = ddlvt2.SelectedValue;
            string dob = txtdob2.Text.Trim();
            string gender = ddlgender2.SelectedValue;
            string status = ddlstatus.SelectedValue;
            string currentProfilePic = fuppicture3.Value;
            string connectionString = ConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Tutor SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Bio = @Bio, Email = @Email, MobileNumber = @MobileNumber, StrAddress = @StrAddress, VillageTown = @VillageTown, District = @District, DateOfBirth = @DateOfBirth, Gender = @Gender, Status = @Status, ProfilePicture = @ProfilePicture WHERE Tutor_ID = @Tutor_ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@MiddleName", middleName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Bio", bio);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@MobileNumber", mobileNumber);
                    command.Parameters.AddWithValue("@StrAddress", street);
                    command.Parameters.AddWithValue("@District", district);
                    command.Parameters.AddWithValue("@VillageTown", vilagetown);
                    command.Parameters.AddWithValue("@DateOfBirth", dob);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Tutor_ID", Tutor_Id);

                    if (fuppicture7.HasFile)
                    {
                        string fileName = Path.GetFileName(fuppicture7.PostedFile.FileName);
                        string filePath = Server.MapPath("~/tImages/" + fileName);
                        fuppicture7.SaveAs(filePath);
                        command.Parameters.AddWithValue("@ProfilePicture", "~/tImages/" + fileName);
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

        protected void LoadCountry2()
        {
            string query = "SELECT Country_Id, Country_Name FROM [dbo].[Country]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlcountry2.DataSource = reader;
                    ddlcountry2.DataTextField = "Country_Name";
                    ddlcountry2.DataValueField = "Country_Id";
                    ddlcountry2.DataBind();
                    reader.Close();
                }
            }

            ddlcountry2.Items.Insert(0, new ListItem("Select country", ""));
        }

        protected void LoadGender2()
        {
            string query = "SELECT Gender_Id, Gender FROM [dbo].[Gender]";

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlgender2.DataSource = reader;
                    ddlgender2.DataTextField = "Gender";
                    ddlgender2.DataValueField = "Gender_Id";
                    ddlgender2.DataBind();
                    reader.Close();
                }
            }

            ddlgender2.Items.Insert(0, new ListItem("Select gender", ""));
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

        protected void ddldistrict_SelectedIndexChanged2(object sender, EventArgs e)
        {
            if (int.TryParse(ddldistrict2.SelectedValue, out int districtId) && districtId != -1)
            {
                PopulateVillageTownList2(ddlvt2, districtId);
            }
            else
            {
                ddlvt2.Items.Clear();
                ddlvt2.Items.Insert(0, new ListItem("Select village/town", "-1"));
            }
        }
    }
}
