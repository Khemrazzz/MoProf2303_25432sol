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
    public partial class studentEditProfilePage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rvdob.MinimumValue = DateTime.Now.AddYears(-65).ToShortDateString();
                rvdob.MaximumValue = DateTime.Now.AddYears(-10).ToShortDateString();
                rvdob.Type = ValidationDataType.Date;

                LoadGender();
                PopulateEducationStatusList(ddlcsts);
                PopulateDistrictList(ddldistrict);

                if (string.IsNullOrEmpty(Convert.ToString(Session["Student_Id"])))
                {
                    Response.Redirect("LogInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else if (Session["Student_Id"] != null)
                {
                    LoadStudentData();
                }
            }
        }

        private void LoadStudentData()
        {
            string Student_Id = Session["Student_Id"].ToString();
            using (SqlConnection connection = new SqlConnection(_conString))
            {
                connection.Open();
                string query = "SELECT FirstName, MiddleName, LastName, Bio, Email, MobileNumber, StrAddress, District, VillageTown, DateOfBirth, Gender, EduStatus, EduGrade, ProfilePicture FROM Student WHERE Student_Id = @Student_Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Student_Id", Student_Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtfname.Text = reader["FirstName"].ToString();
                            txtmname.Text = reader["MiddleName"].ToString();
                            txtlname.Text = reader["LastName"].ToString();
                            txtbio.Text = reader["Bio"].ToString();
                            txtemail.Text = reader["Email"].ToString();
                            txtmob.Text = reader["MobileNumber"].ToString();
                            txtstreetaddress.Text = reader["StrAddress"].ToString();
                            ddldistrict.SelectedValue = reader["District"].ToString();
                            PopulateVillageTownList(ddlvt, Convert.ToInt32(reader["District"]));
                            ddlvt.SelectedValue = reader["VillageTown"].ToString();
                            txtdob.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                            ddlcsts.SelectedValue = reader["EduStatus"].ToString();
                            PopulateGradeList(ddlgrade, Convert.ToInt32(reader["EduStatus"]));
                            ddlgrade.SelectedValue = reader["EduGrade"].ToString();
                            ddlgender.SelectedValue = reader["Gender"].ToString();
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
            string Student_Id = Session["Student_Id"].ToString();
            string firstName = txtfname.Text.Trim();
            string middleName = txtmname.Text.Trim();
            string lastName = txtlname.Text.Trim();
            string bio = txtbio.Text.Trim();
            string email = txtemail.Text.Trim();
            string mobileNumber = txtmob.Text.Trim();
            string street = txtstreetaddress.Text.Trim();
            string district = ddldistrict.SelectedValue;
            string vilagetown = ddlvt.SelectedValue;
            string dob = txtdob.Text.Trim();
            string gender = ddlgender.SelectedValue;
            string edustatus = ddlcsts.SelectedValue;
            string edugrade = ddlgrade.SelectedValue;

            string profilePicPath = imgProfilePicture.ImageUrl; // Keep existing picture path

            if (fuppicture.HasFile)
            {
                string fileName = Path.GetFileName(fuppicture.PostedFile.FileName);
                string filePath = Server.MapPath("~/stdImages/" + fileName);
                fuppicture.SaveAs(filePath);
                profilePicPath = "~/stdImages/" + fileName;
            }

            using (SqlConnection connection = new SqlConnection(_conString))
            {
                connection.Open();
                string query = "UPDATE Student SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Bio = @Bio, EduStatus = @EduStatus, EduGrade = @EduGrade, Email = @Email, MobileNumber = @MobileNumber, StrAddress = @StrAddress, VillageTown = @VillageTown, District = @District, DateOfBirth = @DateOfBirth, Gender = @Gender, ProfilePicture = @ProfilePicture WHERE Student_Id = @Student_Id";
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
                    command.Parameters.AddWithValue("@EduStatus", edustatus);
                    command.Parameters.AddWithValue("@EduGrade", edugrade);
                    command.Parameters.AddWithValue("@ProfilePicture", profilePicPath);
                    command.Parameters.AddWithValue("@Student_Id", Student_Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    lblMessage.Text = rowsAffected > 0 ? "Profile updated successfully." : "Error updating profile.";
                    lblMessage.ForeColor = rowsAffected > 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                }
            }
        }

        protected void LoadGender()
        {
            string query = "SELECT Gender_Id, Gender FROM [dbo].[Gender]";
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlgender.DataSource = reader;
                    ddlgender.DataTextField = "Gender";
                    ddlgender.DataValueField = "Gender_Id";
                    ddlgender.DataBind();
                    reader.Close();
                }
            }
            ddlgender.Items.Insert(0, new ListItem("Select gender", ""));
        }

        private void PopulateEducationStatusList(DropDownList dropdown)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("ViewEducationStatus", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "Current_Status";
                dropdown.DataValueField = "Sts_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Choose status", "-1"));
            }
        }

        private void PopulateGradeList(DropDownList dropdown, int Sts_Id)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand scmd = new SqlCommand("ViewGradeByStatus", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                scmd.Parameters.AddWithValue("@Sts_Id", Sts_Id);
                con.Open();
                dropdown.DataSource = scmd.ExecuteReader();
                dropdown.DataTextField = "Grade_Name";
                dropdown.DataValueField = "Grade_Id";
                dropdown.DataBind();
                dropdown.Items.Insert(0, new ListItem("Choose grade", "-1"));
            }
        }

        protected void ddlcsts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlcsts.SelectedValue, out int Sts_Id) && Sts_Id != -1)
            {
                PopulateGradeList(ddlgrade, Sts_Id);
            }
            else
            {
                ddlgrade.Items.Clear();
                ddlgrade.Items.Insert(0, new ListItem("Choose grade", "-1"));
            }
        }

        private void PopulateDistrictList(DropDownList dropdown)
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

        private void PopulateVillageTownList(DropDownList dropdown, int districtId)
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
                PopulateVillageTownList(ddlvt, districtId);
            }
            else
            {
                ddlvt.Items.Clear();
                ddlvt.Items.Insert(0, new ListItem("Select village/town", "-1"));
            }
        }
    }
}
