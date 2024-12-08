using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.IO;

namespace MoProf2303_25432
{
    public partial class tutorAddCoursePage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSeat();
                LoadMode();
                BindDays();
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

                }
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            string tutor_Id = Session["Tutor_Id"]?.ToString();
            if (string.IsNullOrEmpty(tutor_Id))
            {
                Response.Redirect("logInPage.aspx");
                return;
            }

            string fileName = null;
            if (fuppicture4.HasFile)
            {
                try
                {
                    string folderPath = Server.MapPath("~/coursePicturesImage/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    fileName = Path.GetFileName(fuppicture4.PostedFile.FileName);
                    fuppicture4.SaveAs(folderPath + fileName);
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "File upload failed: " + ex.Message;
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                string query = "INSERT INTO Courses (Tutor_Id, Subject_Name, Description, Fees, Date, DayName, TimeSlot, ModeName, District, VillageTown, StrAddress, SeatCount, CoursePicture, Category_Id) VALUES (@Tutor_Id, @Subject_Name, @Description, @Fees, @Date, @DayName, @TimeSlot, @ModeName, @District, @VillageTown, @StrAddress, @SeatCount, @CoursePicture, @Category_Id)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Tutor_Id", tutor_Id);
                cmd.Parameters.AddWithValue("@Subject_Name", txtcname.Text);
                cmd.Parameters.AddWithValue("@Description", txtbio2.Text);
                cmd.Parameters.AddWithValue("@Fees", txtfee.Text);
                cmd.Parameters.AddWithValue("@Date", txtdate.Text);
                cmd.Parameters.AddWithValue("@DayName", ddlday.SelectedValue);
                cmd.Parameters.AddWithValue("@TimeSlot", ddltime.SelectedValue);
                cmd.Parameters.AddWithValue("@ModeName", ddlmode.SelectedValue);
                cmd.Parameters.AddWithValue("@District", ddldistrict2.SelectedValue);
                cmd.Parameters.AddWithValue("@VillageTown", ddlvt2.SelectedValue);
                cmd.Parameters.AddWithValue("@StrAddress", txtstreetaddress2.Text);
                cmd.Parameters.AddWithValue("@SeatCount", ddlseat.SelectedValue);
                cmd.Parameters.AddWithValue("@CoursePicture", fileName != null ? "~/coursePicturesImage/" + fileName : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Category_Id", ddlCategory.SelectedValue); // Add this line to save category
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("tutorDashboardPage.aspx");
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

        protected void LoadCategories()
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
                    ddltime.DataSource = cmd.ExecuteReader();
                    ddltime.DataTextField = "TimeSlot";
                    ddltime.DataValueField = "TimeSlot_ID";
                    ddltime.DataBind();
                    con.Close();
                }
            }
            ddltime.Items.Insert(0, new ListItem("Select time slot", "-1"));
        }
    }
}