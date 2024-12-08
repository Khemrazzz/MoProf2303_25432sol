using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class userMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckUserLogin();
            }
        }

        private void CheckUserLogin()
        {
            if (Session["Student_Id"] != null)
            {
                phLoginButtons.Visible = false;
                phProfileButton.Visible = true;
                hlProfile.NavigateUrl = "~/studentDashboardPage.aspx";
            }
            else if (Session["Tutor_Id"] != null)
            {
                phLoginButtons.Visible = false;
                phProfileButton.Visible = true;
                hlProfile.NavigateUrl = "~/tutorDashboardPage.aspx";
            }
            else if (Session["Advertiser_Id"] != null)
            {
                phLoginButtons.Visible = false;
                phProfileButton.Visible = true;
                hlProfile.NavigateUrl = "~/adsDashboardPage.aspx";
            }
            else if (Session["Admin_Id"] != null)
            {
                phLoginButtons.Visible = false;
                phProfileButton.Visible = true;
                hlProfile.NavigateUrl = "~/adminDashboardPage.aspx";
            }
            else
            {
                phLoginButtons.Visible = true;
                phProfileButton.Visible = false;
            }
        }
    }
}