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
    public partial class tutorEditCoursePage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSeat();
                LoadMode();
                BindDays();
                BindTimeSlots();
                PopulateDistrictList2(ddldistrict2);
                LoadCategories();

                rvdob2.MinimumValue = DateTime.Now.ToShortDateString();
                rvdob2.MaximumValue = DateTime.Now.AddYears(1).ToShortDateString();
                rvdob2.Type = ValidationDataType.Date;

                if (string.IsNullOrEmpty(Convert.ToString(Session["Tutor_Id"])))
                {
                    Response.Redirect("logInPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                else
                {
                    string TutorId = Session["Tutor_Id"].ToString();
                    LoadCourseDetails();
                }
            }
        }

        private void LoadCategories()
        {
            string query = "SELECT Category_Id, Category_Name FROM [dbo].[Category]";
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlCategory.DataSource = reader;
                    ddlCategory.DataTextField = "Category_Name";
                    ddlCategory.DataValueField = "Category_Id";
                    ddlCategory.DataBind();
                    reader.Close();
                }
            }
            ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
        }

        private void LoadCourseDetails()
        {
            string courseId = Request.QueryString["CourseId"];
            if (string.IsNullOrEmpty(courseId))
            {
                lblMessage.Text = "Course ID is missing.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = @"
                SELECT 
                    Subject_Name, Description, Fees, Date, DayName, 
                    TimeSlot, ModeName, District, VillageTown, 
                    StrAddress, SeatCount, CoursePicture, Category_Id
                FROM Courses 
                WHERE Course_Id = @CourseId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtcname.Text = reader["Subject_Name"].ToString();
                        txtbio2.Text = reader["Description"].ToString();
                        txtfee.Text = reader["Fees"].ToString();
                        txtdate.Text = Convert.ToDateTime(reader["Date"]).ToString("yyyy-MM-dd");
                        ddlday.SelectedValue = reader["DayName"].ToString();
                        ddltime.SelectedValue = reader["TimeSlot"].ToString();
                        ddlmode.SelectedValue = reader["ModeName"].ToString();
                        ddldistrict2.SelectedValue = reader["District"].ToString();
                        PopulateVillageTownList2(ddlvt2, Convert.ToInt32(reader["District"]));
                        ddlvt2.SelectedValue = reader["VillageTown"].ToString();
                        txtstreetaddress2.Text = reader["StrAddress"].ToString();
                        ddlseat.SelectedValue = reader["SeatCount"].ToString();
                        imgCoursePicture.ImageUrl = reader["CoursePicture"].ToString();
                        fuppicture3.Value = reader["CoursePicture"].ToString();
                        ddlCategory.SelectedValue = reader["Category_Id"].ToString(); // Add this line to set the selected category
                    }
                    else
                    {
                        lblMessage.Text = "Course not found.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            string courseId = Request.QueryString["CourseId"];
            string courseName = txtcname.Text.Trim();
            string description = txtbio2.Text.Trim();
            string fees = txtfee.Text.Trim();
            string date = txtdate.Text.Trim();
            string day = ddlday.SelectedValue;
            string time = ddltime.SelectedValue;
            string mode = ddlmode.SelectedValue;
            string district = ddldistrict2.SelectedValue;
            string villageTown = ddlvt2.SelectedValue;
            string streetAddress = txtstreetaddress2.Text.Trim();
            string seats = ddlseat.SelectedValue;
            string currentCoursePic = fuppicture3.Value;
            string categoryId = ddlCategory.SelectedValue; // Add this line to get the selected category

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                string query = "UPDATE Courses SET Subject_Name = @CourseName, Description = @Description, Fees = @Fees, Date = @Date, DayName = @Day, TimeSlot = @Time, ModeName = @Mode, District = @District, VillageTown = @VillageTown, StrAddress = @StreetAddress, SeatCount = @Seats, CoursePicture = @CoursePicture, Category_Id = @CategoryId WHERE Course_Id = @CourseId"; // Update query to include Category_Id
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseName", courseName);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Fees", fees);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Day", day);
                    cmd.Parameters.AddWithValue("@Time", time);
                    cmd.Parameters.AddWithValue("@Mode", mode);
                    cmd.Parameters.AddWithValue("@District", district);
                    cmd.Parameters.AddWithValue("@VillageTown", villageTown);
                    cmd.Parameters.AddWithValue("@StreetAddress", streetAddress);
                    cmd.Parameters.AddWithValue("@Seats", seats);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId); // Add this line to save the selected category
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    if (fuppicture4.HasFile)
                    {
                        string fileName = Path.GetFileName(fuppicture4.PostedFile.FileName);
                        string filePath = Server.MapPath("~/tImages/" + fileName);
                        fuppicture4.SaveAs(filePath);
                        cmd.Parameters.AddWithValue("@CoursePicture", "~/tImages/" + fileName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CoursePicture", currentCoursePic);
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();
                    lblMessage.Text = rowsAffected > 0 ? "Course updated successfully." : "Error updating course.";
                    lblMessage.ForeColor = rowsAffected > 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                }
            }
        }

        protected void LoadMode()
        {
            string query = "SELECT Mode_Id, ModeName FROM [dbo].[Mode]";
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlmode.DataSource = reader;
                    ddlmode.DataTextField = "ModeName";
                    ddlmode.DataValueField = "ModeName";
                    ddlmode.DataBind();
                    reader.Close();
                }
            }
            ddlmode.Items.Insert(0, new ListItem("Select Mode", ""));
        }

        protected void LoadSeat()
        {
            string query = "SELECT Seat_Id, SeatCount FROM [dbo].[Seats]";
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlseat.DataSource = reader;
                    ddlseat.DataTextField = "SeatCount";
                    ddlseat.DataValueField = "SeatCount";
                    ddlseat.DataBind();
                    reader.Close();
                }
            }
            ddlseat.Items.Insert(0, new ListItem("Select seats", ""));
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

        protected void ddldistrict2_SelectedIndexChanged(object sender, EventArgs e)
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

        private void BindDays()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllDays", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    ddlday.DataSource = cmd.ExecuteReader();
                    ddlday.DataTextField = "DayName";
                    ddlday.DataValueField = "Day_Id";
                    ddlday.DataBind();
                    con.Close();
                }
            }
            ddlday.Items.Insert(0, new ListItem("Select day", "-1"));
        }

        protected void ddlday_SelectedIndexChanged2(object sender, EventArgs e)
        {
            BindTimeSlots();
        }

        private void BindTimeSlots()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand("GetTimeSlots", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddltime.DataSource = reader;
                    ddltime.DataTextField = "TimeSlot";
                    ddltime.DataValueField = "TimeSlot_ID"; // Ensure this matches the column name returned by the stored procedure
                    ddltime.DataBind();
                    con.Close();
                }
            }
            ddltime.Items.Insert(0, new ListItem("Select time slot", ""));
        }
    }
}
